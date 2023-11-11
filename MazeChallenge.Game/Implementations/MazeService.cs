﻿using MazeChallenge.Domain.DTO;
using MazeChallenge.Domain.Entities;
using MazeChallenge.Persistence.UnitOfWork;
using MazeChallenge.Domain.Mappers;

namespace MazeChallenge.Game.Implementations
{
    public class MazeService : BaseService
    {
		public MazeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
		}

        public async Task CreateNewMaze(int height, int width)
        {
            var randomGenerator = new Random();
            height = height != 0 ? height : randomGenerator.Next(1,25);
            width = width != 0 ? width : randomGenerator.Next(1,25);

            await GenerateMaze(height, width);
            await GenerateBlocks(height*width);
            await _unitOfWork.SaveAsync();
        }

        public async Task<MazeDto> SeeMaze(Guid mazeUuid)
        {
            var maze = await _unitOfWork.MazeRepository.FindAsync(mazeUuid);
            return maze.MapToMazeDto();
        }

        private async Task GenerateMaze(int height, int width)
        {
            var maze = new Maze
            {
                //Uuid = 
                Height = height,
                Width = width,
            };

            await _unitOfWork.MazeRepository.AddAsync(maze);
        }

        private async Task GenerateBlocks(int mazeSize)
        {
            // TODO: Strengthen this logic later

            for (var cell = 0; cell < mazeSize; cell++)
            {
                var block = new Block
                {
                    //Uuid = 
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
	}
}

