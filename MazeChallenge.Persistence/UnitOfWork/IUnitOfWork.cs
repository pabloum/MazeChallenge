using MazeChallenge.Persistence.Contracts;

namespace MazeChallenge.Persistence.UnitOfWork
{
    public interface IUnitOfWork
	{
        IMazeRepository MazeRepository { get; }
        IBlockRepository BlockRepository { get; }
        IGameRepository GameRepository { get; }

        Task SaveAsync();
    }
}

