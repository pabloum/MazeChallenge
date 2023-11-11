using MazeChallenge.Persistence.UnitOfWork;

namespace MazeChallenge.Game.Implementations
{
    public class MazeService : BaseService
    {
		public MazeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
		}

        public async Task CreateNewMaze()
        {

        }
	}
}

