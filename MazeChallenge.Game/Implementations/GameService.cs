using MazeChallenge.Domain.Constants;
using MazeChallenge.Domain.DTO;
using MazeChallenge.Domain.Entities;
using MazeChallenge.Domain.Enums;
using MazeChallenge.Domain.Exceptions;
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

        /// <summary>
        /// This method is for testing purposes
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<GameDto>> GetAll()
        {
            var games = await _unitOfWork.GameRepository.GetAllAsync(null, null, "CurrentBlock");
            return games.Select(g => g.MapToGameDto());
        }


        public async Task<GameDto> CreateNewGameWithExistingMaze(Guid mazeUuid)
		{
            var newGame = CreateNewGame(mazeUuid);
            await _unitOfWork.GameRepository.AddAsync(newGame);
            await _unitOfWork.SaveAsync();
            return newGame.MapToGameDto();
        }

        public async Task<GameDto> CreateNewGameWithNewMaze()
        {
            var mazeCreatedDto = await _mazeService.CreateNewMaze(25, 25);
            var newGame = CreateNewGame(mazeCreatedDto.MazeUuid);
            await _unitOfWork.GameRepository.AddAsync(newGame);
            await _unitOfWork.SaveAsync();

            return newGame.MapToGameDto();
        }

        public async Task<GameLookDto> TakeALook(Guid mazeUuid, Guid gameUuid)
        {
            var game = await _unitOfWork.GameRepository.FindAsync(gameUuid);

            if (game == null)
            {
                throw new ValidationException(Constants.InvalidGameUid);
            }

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
                CheckNextMove(operation, game.CurrentBlock);

                game.CurrentBlock.CoordX += Action[operation].X;
                game.CurrentBlock.CoordY += Action[operation].Y;
            }

            if (WasMazeCompleted(game))
            {
                game.Completed = true;
            }

            await _unitOfWork.SaveAsync();
            return game.MapToGameLookDto();
        }

        private void CheckNextMove(Operation operation, Block block)
        {
            var checker = new Dictionary<Operation, bool>
            {
                { Operation.GoNorth, block.NorthBlocked },
                { Operation.GoSouth, block.SouthBlocked },
                { Operation.GoWest, block.WestBlocked },
                { Operation.GoEast, block.EastBlocked },
            };

            if (checker[operation])
            {
                throw new ValidationException(Constants.InvalidMove);
            }
        }

        private bool WasMazeCompleted(Domain.Entities.Game game)
        {
            var currentPosition = game.CurrentBlock;
            return currentPosition.CoordX == game.Maze.Width && currentPosition.CoordY == game.Maze.Height;
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

