using MazeChallenge.Domain.Enums;

namespace MazeChallenge.Game.Contracts
{
    public interface IGameService : IService
    {
        Task CreateNewGameWithExistingMaze(Guid mazeUuid);
        Task CreateNewGameWithNewMaze();
        Task TakeALook(Guid mazeUuid, Guid gameUuid);
        Task MoveNextCell(Guid mazeUuid, Guid gameUuid, Operation operation);
        Task ResetGame(Guid mazeUuid, Guid gameUuid);
    }
}

