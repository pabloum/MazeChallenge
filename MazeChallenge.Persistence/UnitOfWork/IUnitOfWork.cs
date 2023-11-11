using System;
using MazeChallenge.Domain.Entities;
using MazeChallenge.Persistence.Contracts;

namespace MazeChallenge.Persistence.UnitOfWork
{
	public interface IUnitOfWork
	{
        IBaseRepository<Maze> MazeRepository { get; }
        IBaseRepository<Block> BlockRepository { get; }
        IBaseRepository<Game> GameRepository { get; }

        Task SaveAsync();
    }
}

