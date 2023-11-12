using MazeChallenge.Domain.Context;
using MazeChallenge.Domain.Entities;
using MazeChallenge.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace MazeChallenge.Persistence.Implementations
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
	{
		public GameRepository(MazeDbContext context) : base(context)
		{
		}
    }
}

