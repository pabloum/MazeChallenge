using MazeChallenge.Domain.Context;
using MazeChallenge.Domain.Entities;
using MazeChallenge.Persistence.Contracts;

namespace MazeChallenge.Persistence.Implementations
{
    public class MazeRepository : BaseRepository<Maze>, IMazeRepository
	{
		public MazeRepository(MazeDbContext context) : base(context)
        {
        }
    }
}

