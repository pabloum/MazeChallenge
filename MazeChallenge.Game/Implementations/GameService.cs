using MazeChallenge.Domain.DTO;
using MazeChallenge.Domain.Enums;
using MazeChallenge.Domain.Mappers;
using MazeChallenge.Game.Contracts;
using MazeChallenge.Persistence.UnitOfWork;

namespace MazeChallenge.Game.Implementations
{
    public class GameService : BaseService
    {
        private readonly IMazeService _mazeService;

        private readonly Dictionary<Operation, (int X, int Y)> Action = new Dictionary<Operation, (int X, int Y)>
        {
            { Operation.Start, (0, 0) },
            { Operation.GoNorth, (0, 1) },
            { Operation.GoSouth, (0, -1) },
            { Operation.GoWest, (-1, 0) },
            { Operation.GoEast, (1, 0) },
        };

        public GameService(IUnitOfWork unitOfWork, IMazeService mazeService) : base(unitOfWork)
		{
            _mazeService = mazeService;
        }

		public async Task<GameDto> CreateNewGameWithExistingMaze(Guid mazeUuid)
		{
            var maze = await _unitOfWork.MazeRepository.FindAsync(mazeUuid);
            var newGame = CreateNewGame(mazeUuid);
            await _unitOfWork.GameRepository.AddAsync(newGame);

            return newGame.MapToGameDto();
        }

        public async Task<GameDto> CreateNewGameWithNewMaze()
        {
            var mazeUuid = await _mazeService.CreateNewMaze(25, 25);
            var newGame = CreateNewGame(mazeUuid);
            await _unitOfWork.GameRepository.AddAsync(newGame);

            return newGame.MapToGameDto();
        }

        public async Task<GameDto> TakeALook(Guid mazeUuid, Guid gameUuid)
        {
            var game = await _unitOfWork.GameRepository.FindAsync(gameUuid);
            var maze = await _unitOfWork.MazeRepository.FindAsync(mazeUuid);

            return game.MapToGameDto();
        }

        public async Task<GameDto> MoveNextCell(Guid mazeUuid, Guid gameUuid, Operation operation)
        {
            var game = await _unitOfWork.GameRepository.FindAsync(gameUuid);
            var maze = await _unitOfWork.MazeRepository.FindAsync(mazeUuid);

            if (operation == Operation.Start)
            {
                game.CurrentPositionX = 0;
                game.CurrentPositionY = 0;
            }
            else
            {
                game.CurrentPositionX += Action[operation].X;
                game.CurrentPositionY += Action[operation].Y;
            }

            await _unitOfWork.SaveAsync();

            return game.MapToGameDto();
        }

        private Domain.Entities.Game CreateNewGame(Guid mazeUuid)
        {
            return new Domain.Entities.Game
            {
                MazeUuid = mazeUuid,
                Completed = false,
                CurrentPositionX = 0,
                CurrentPositionY = 0,
            };
        }
    }
}

