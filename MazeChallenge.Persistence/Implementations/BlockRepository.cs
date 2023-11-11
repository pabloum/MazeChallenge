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
    }
}

