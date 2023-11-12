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

        public async Task<MazeDto> SeeMaze(Guid mazeUuid)
        {
            var maze = await _unitOfWork.MazeRepository.FindAsync(mazeUuid);
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
            var mazeSize = maze.Height * maze.Width;

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
            // TODO: Strengthen this logic later

            var block = new Block
            {
                MazeUuid = mazeUuid,
                CoordX = x,
                CoordY = y,
                NorthBlocked = true,
                SouthBlocked = false,
                WestBlocked = true,
                EastBlocked = false,
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

