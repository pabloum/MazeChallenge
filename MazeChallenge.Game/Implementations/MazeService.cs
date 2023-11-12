﻿using MazeChallenge.Domain.DTO;
using MazeChallenge.Domain.Entities;
using MazeChallenge.Domain.Exceptions;
using MazeChallenge.Persistence.UnitOfWork;
using MazeChallenge.Domain.Mappers;

namespace MazeChallenge.Game.Implementations
{
    public class MazeService : BaseService
    {
		public MazeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
		}

        public async Task<Guid> CreateNewMaze(int height, int width)
        {
            ValidateDimensions(height, width);

            var mazeUuid = await GenerateMaze(height, width);
            await GenerateBlocks(height*width);
            await _unitOfWork.SaveAsync();

            return mazeUuid;
        }

        public async Task<MazeDto> SeeMaze(Guid mazeUuid)
        {
            var maze = await _unitOfWork.MazeRepository.FindAsync(mazeUuid);
            return maze.MapToMazeDto();
        }

        private async Task<Guid> GenerateMaze(int height, int width)
        {
            var maze = new Maze
            {
                Height = height,
                Width = width,
            };

            await _unitOfWork.MazeRepository.AddAsync(maze);
            return Guid.Empty;
        }

        private async Task GenerateBlocks(int mazeSize)
        {
            // TODO: Strengthen this logic later

            for (var cell = 0; cell < mazeSize; cell++)
            {
                var block = new Block
                {
                    CoordX = 0,
                    CoordY = 0,
                    NorthBlocked = true,
                    SouthBlocked = false,
                    WestBlocked = true,
                    EastBlocked = false,
                    //MazeUuid = 
                };

                await _unitOfWork.BlockRepository.AddAsync(block);
            }
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

