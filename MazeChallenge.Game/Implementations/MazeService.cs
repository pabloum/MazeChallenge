using MazeChallenge.Domain.DTO;
using MazeChallenge.Domain.Entities;
using MazeChallenge.Domain.Exceptions;
using MazeChallenge.Persistence.UnitOfWork;
using MazeChallenge.Domain.Mappers;
using MazeChallenge.Game.Contracts;
using MazeChallenge.Domain.Constants;

namespace MazeChallenge.Game.Implementations
{
    public class MazeService : BaseService, IMazeService
    {
		public MazeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
		}

        /// <summary>
        /// This method is for testing purposes
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MazeDto>> GetAll()
        {
            var games = await _unitOfWork.MazeRepository.GetAllAsync(null, null, "Blocks");
            return games.Select(m => m.MapToMazeDto());
        }

        public async Task<MazeCreatedDto> CreateNewMaze(int height, int width)
        {
            ValidateDimensions(height, width);

            var maze = await GenerateMaze(height, width);
            await GenerateBlocks(maze);
            await _unitOfWork.SaveAsync();

            return maze.MapToMazeCreatedDto();
        }

        public MazeDto SeeMaze(Guid mazeUuid)
        {
            var maze = _unitOfWork.MazeRepository.GetEntity(mazeUuid, "Blocks");

            if (maze == null)
            {
                throw new ValidationException(Constants.InvalidMazeUid);
            }

            return maze.MapToMazeDto();
        }

        private async Task<Maze> GenerateMaze(int height, int width)
        {
            var maze = new Maze
            {
                Height = height,
                Width = width,
            };

            var createdMaze = await _unitOfWork.MazeRepository.AddAsync(maze);
            return createdMaze;
        }

        private async Task GenerateBlocks(Maze maze)
        {
            for (var x = 0; x < maze.Width; x++)
            {
                for (var y = 0; y < maze.Height; y++)
                {
                    var block = GenerateBlock(maze.Uuid, x, y);
                    await _unitOfWork.BlockRepository.AddAsync(block);
                }
            }
        }

        private Block GenerateBlock(Guid mazeUuid, int x, int y)
        {
            // TODO: Create logic for defining walls
            var walls = new Dictionary<int, bool>
            {
                { 0, true },
                { 1, false },
                { 2, true },
                { 3, false },
            };

            var block = new Block
            {
                MazeUuid = mazeUuid,
                CoordX = x,
                CoordY = y,
                NorthBlocked = walls[0],
                SouthBlocked = walls[1],
                WestBlocked = walls[2],
                EastBlocked = walls[3],
            };

            return block;
        }

        private void ValidateDimensions(int height, int width)
        {
            if (height <= 0 || width <= 0)
            {
                throw new ValidationException(Constants.InvalidDimensions);
            }
        }
    }
}

