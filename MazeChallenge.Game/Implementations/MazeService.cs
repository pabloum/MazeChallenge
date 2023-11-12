using MazeChallenge.Domain.DTO;
using MazeChallenge.Domain.Entities;
using MazeChallenge.Domain.Exceptions;
using MazeChallenge.Persistence.UnitOfWork;
using MazeChallenge.Domain.Mappers;
using MazeChallenge.Game.Contracts;

namespace MazeChallenge.Game.Implementations
{
    public class MazeService : BaseService, IMazeService
    {
		public MazeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
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
            
            for (var cell = 0; cell < mazeSize; cell++)
            {
                var block = GenerateBlock(maze.Uuid);
                await _unitOfWork.BlockRepository.AddAsync(block);
            }
        }

        private Block GenerateBlock(Guid mazeUuid)
        {
            // TODO: Strengthen this logic later

            var block = new Block
            {
                MazeUuid = mazeUuid,
                CoordX = 0,
                CoordY = 0,
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
                // Todo: Move this message to a constants file
                throw new ValidationException("Invalid Dimensions. Both height and width should be greater than 0");
            }
        }
    }
}

