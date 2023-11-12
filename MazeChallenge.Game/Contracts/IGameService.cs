using MazeChallenge.Domain.DTO;
using MazeChallenge.Domain.Enums;

namespace MazeChallenge.Game.Contracts
{
    public interface IGameService : IService
    {
        Task<GameDto> CreateNewGameWithExistingMaze(Guid mazeUuid);
        Task<GameDto> CreateNewGameWithNewMaze();
        Task<GameLookDto> TakeALook(Guid mazeUuid, Guid gameUuid);
        Task<GameLookDto> MoveNextCell(Guid mazeUuid, Guid gameUuid, Operation operation);
    }
}

