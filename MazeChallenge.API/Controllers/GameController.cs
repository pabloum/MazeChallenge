using System;
using MazeChallenge.Domain.Enums;
using MazeChallenge.Game.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace MazeChallenge.API.Controllers
{
	public class GameController : BaseController
	{
		private IGameService _gameService;

		public GameController(IGameService gameService)
		{
			_gameService = gameService;
        }

		[HttpPost("{mazeId}")]
		public async Task<IActionResult> CreateGame(Guid mazeId)
		{
			await _gameService.CreateNewGameWithExistingMaze(mazeId);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame()
        {
            await _gameService.CreateNewGameWithNewMaze();
            return Ok();
        }

        [HttpGet("{mazeId}/{gameUuid}")]
        public async Task<IActionResult> TakeALook(Guid mazeId, Guid gameUuid)
        {
            await _gameService.TakeALook(mazeId, gameUuid);
            return Ok();
        }

        [HttpPost("{mazeId}/{gameUuid}")]
        public async Task<IActionResult> MoveNextCell(Guid mazeId, Guid gameUuid, [FromBody]Operation operation)
        {
            await _gameService.MoveNextCell(mazeId, gameUuid, operation);
            return Ok();
        }
    }
}

