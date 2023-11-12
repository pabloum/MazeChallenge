using MazeChallenge.Domain.DTO;

namespace MazeChallenge.Game.Contracts
{
    public interface IMazeService : IService
	{
        Task<IEnumerable<MazeDto>> GetAll();
        Task<MazeCreatedDto> CreateNewMaze(int height, int width);
        MazeDto SeeMaze(Guid mazeUuid);
    }
}

