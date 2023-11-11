using System;
using Microsoft.AspNetCore.Mvc;

namespace MazeChallenge.API.Controllers
{
	public class MazeController : BaseController
	{
		public MazeController()
		{
		}

        [HttpPost]
        public async Task<IActionResult> CreateMaze(int height, int width)
        {
            return Ok();
        }
    }
}

