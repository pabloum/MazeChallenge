using System;
namespace MazeChallenge.Game.Contracts
{
	public interface IMazeService : IService
	{
        Task CreateNewMaze();
    }
}

