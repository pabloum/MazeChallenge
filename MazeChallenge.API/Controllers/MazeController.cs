using System;
using MazeChallenge.Game.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace MazeChallenge.API.Controllers
{
	public class MazeController : BaseController
	{
        private IMazeService _mazeService;

        public MazeController(IMazeService mazeService)
		{
            _mazeService = mazeService;
		}

        [HttpPost]
        public async Task<IActionResult> CreateMaze(int height = 25, int width = 25)
        {
            var maze = await _mazeService.CreateNewMaze(height, width);
            return Ok(maze);
        }

        [HttpGet]
        public async Task<IActionResult> SeeMaze(Guid mazeUuid)
        {
            var maze = await _mazeService.SeeMaze(mazeUuid);
            return Ok(maze);
        }
    }
}

