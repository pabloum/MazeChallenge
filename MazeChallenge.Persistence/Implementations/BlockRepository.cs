using MazeChallenge.Domain.Context;
using MazeChallenge.Domain.Entities;
using MazeChallenge.Persistence.Contracts;

namespace MazeChallenge.Persistence.Implementations
{
    public class BlockRepository : BaseRepository<Block>, IBlockRepository
    {
        public BlockRepository(MazeDbContext context) : base(context)
        {
        }

        public Block FindCurrent(Guid mazeUuid)
        {
            return _dbSet.Where(b => b.MazeUuid == mazeUuid && b.IsCurrent).FirstOrDefault();
        }

        public Block GetInitialBlock(Guid mazeUuid)
        {
            return _dbSet.Where(b => b.CoordX == 0 && b.CoordY == 0).FirstOrDefault();
        }
    }
}

