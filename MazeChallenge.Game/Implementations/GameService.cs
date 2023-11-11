using System;
using MazeChallenge.Domain.Enums;
using MazeChallenge.Persistence.UnitOfWork;

namespace MazeChallenge.Game.Implementations
{
	public class GameService : BaseService
    {
		public GameService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public async Task CreateNewGameWithExistingMaze(Guid mazeUuid)
		{

		}

        public async Task CreateNewGameWithNewMaze()
        {

        }

        public async Task TakeALook(Guid mazeUuid, Guid gameUuid)
        {

        }

        public async Task MoveNextCell(Guid mazeUuid, Guid gameUuid, Operation operation)
        {

        }

        public async Task ResetGame(Guid mazeUuid, Guid gameUuid)
        {

        }
    }
}

