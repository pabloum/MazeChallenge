using System;
using MazeChallenge.Domain.Enums;
using MazeChallenge.Game.Contracts;
using MazeChallenge.Game.Implementations;
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
            var game = await _gameService.CreateNewGameWithExistingMaze(mazeId);
            return Ok(game);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame()
        {
            var game = await _gameService.CreateNewGameWithNewMaze();
            return Ok(game);
        }

        [HttpGet("{mazeId}/{gameUuid}")]
        public async Task<IActionResult> TakeALook(Guid mazeId, Guid gameUuid)
        {
            var game = await _gameService.TakeALook(mazeId, gameUuid);
            return Ok(game);
        }

        [HttpPost("{mazeId}/{gameUuid}")]
        public async Task<IActionResult> MoveNextCell(Guid mazeId, Guid gameUuid, [FromBody]Operation operation)
        {
            var game = await _gameService.MoveNextCell(mazeId, gameUuid, operation);
            return Ok(game);
        }

        /// <summary>
        /// This was just for testing purposes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mazes = await _gameService.GetAll();
            return Ok(mazes);
        }
    }
}

