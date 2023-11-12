using System;
using MazeChallenge.Domain.Entities;

namespace MazeChallenge.Persistence.Contracts
{
	public interface IBlockRepository : IBaseRepository<Block>, IRepository
    {
        Block FindCurrent(Guid mazeUuid);
        Block GetInitialBlock(Guid mazeUuid);
    }
}

