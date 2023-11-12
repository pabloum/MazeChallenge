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

        [HttpGet("{mazeUuid}")]
        public IActionResult SeeMaze(Guid mazeUuid)
        {
            var maze = _mazeService.SeeMaze(mazeUuid);
            return Ok(maze);
        }

        /// <summary>
        /// This was just for testing purposes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mazes = await _mazeService.GetAll();
            return Ok(mazes);
        }
    }
}

