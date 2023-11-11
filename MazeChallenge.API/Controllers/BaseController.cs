using System;
using Microsoft.AspNetCore.Mvc;

namespace MazeChallenge.API.Controllers
{
	[ApiController]
	[Route("api/controller")]
	public class BaseController : ControllerBase
	{
		public BaseController()
		{
		}

		[HttpGet]
		public async Task<IActionResult> Test() => Ok();
	}
}

