using MazeChallenge.Domain.DTO;
using MazeChallenge.Domain.Enums;
using MazeChallenge.Domain.Mappers;
using MazeChallenge.Game.Contracts;
using MazeChallenge.Persistence.UnitOfWork;

namespace MazeChallenge.Game.Implementations
{
    public class GameService : BaseService, IGameService
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
            var newGame = CreateNewGame(mazeUuid);
            await _unitOfWork.GameRepository.AddAsync(newGame);

            return newGame.MapToGameDto();
        }

        public async Task<GameDto> CreateNewGameWithNewMaze()
        {
            var mazeCreatedDto = await _mazeService.CreateNewMaze(25, 25);
            var newGame = CreateNewGame(mazeCreatedDto.MazeUuid);
            await _unitOfWork.GameRepository.AddAsync(newGame);

            return newGame.MapToGameDto();
        }

        public async Task<GameLookDto> TakeALook(Guid mazeUuid, Guid gameUuid)
        {
            var game = await _unitOfWork.GameRepository.FindAsync(gameUuid);
            var maze = await _unitOfWork.MazeRepository.FindAsync(mazeUuid);

            return game.MapToGameLookDto();
        }

        public async Task<GameLookDto> MoveNextCell(Guid mazeUuid, Guid gameUuid, Operation operation)
        {
            var game = await _unitOfWork.GameRepository.FindAsync(gameUuid);
            var maze = await _unitOfWork.MazeRepository.FindAsync(mazeUuid);

            if (operation == Operation.Start)
            {
                game.CurrentBlock.CoordX = 0;
                game.CurrentBlock.CoordY = 0;
            }
            else
            {
                // Todo: Check if it move is possible
                game.CurrentBlock.CoordX += Action[operation].X;
                game.CurrentBlock.CoordY += Action[operation].Y;
            }

            await _unitOfWork.SaveAsync();

            // Todo: Check if maze is completed

            return game.MapToGameLookDto();
        }

        private Domain.Entities.Game CreateNewGame(Guid mazeUuid)
        {
            var initialBlock = _unitOfWork.BlockRepository.GetInitialBlock(mazeUuid);

            return new Domain.Entities.Game
            {
                MazeUuid = mazeUuid,
                Completed = false,
                CurrentBlockUuid = initialBlock.Uuid,
                CurrentBlock = initialBlock
            };
        }
    }
}

