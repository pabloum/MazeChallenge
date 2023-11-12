using System;
namespace MazeChallenge.Game.Contracts
{
	public interface IMazeService : IService
	{
        Task<Guid> CreateNewMaze(int height, int width);
        Task SeeMaze(Guid mazeUuid);
    }
}

