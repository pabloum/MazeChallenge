using System;
using Microsoft.AspNetCore.Mvc;

namespace MazeChallenge.API.Controllers
{
	public class GameController : BaseController
	{
		public GameController()
		{
		}

		[HttpPost]
		public async Task<IActionResult> CreateGame(Guid mazeId)
		{
            return Ok();
        }
	}
}

