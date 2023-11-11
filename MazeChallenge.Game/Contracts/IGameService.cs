namespace MazeChallenge.Game.Contracts
{
    public interface IGameService : IService
    {
        Task CreateNewGameWithExistingMaze(Guid mazeUuid);
        Task CreateNewGameWithNewMaze();
        Task TakeALook(Guid mazeUuid, Guid gameUuid);
        Task MoveNextCell(Guid mazeUuid, Guid gameUuid);
        Task ResetGame(Guid mazeUuid, Guid gameUuid);
    }
}

