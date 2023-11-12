using MazeChallenge.Domain.DTO;
using MazeChallenge.Domain.Enums;

namespace MazeChallenge.Game.Contracts
{
    public interface IGameService : IService
    {
        Task<GameDto> CreateNewGameWithExistingMaze(Guid mazeUuid);
        Task<GameDto> CreateNewGameWithNewMaze();
        Task<GameDto> TakeALook(Guid mazeUuid, Guid gameUuid);
        Task<GameDto> MoveNextCell(Guid mazeUuid, Guid gameUuid, Operation operation);
    }
}

