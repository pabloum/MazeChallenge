using MazeChallenge.Game.Contracts;

namespace MazeChallenge.API.Installers
{
    public static class PopupInMemoryDb
	{
        public static async Task Populate(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var mazeService = scope.ServiceProvider.GetRequiredService<IMazeService>();
                var gameService = scope.ServiceProvider.GetRequiredService<IGameService>();

                await PopulateMazeAndGame(mazeService, gameService);
            }
        }
        private async static Task PopulateMazeAndGame(IMazeService mazeService, IGameService gameService)
        {
            var maze1 = await mazeService.CreateNewMaze(3, 4);
            var maze2 = await mazeService.CreateNewMaze(2, 2);

            await gameService.CreateNewGameWithExistingMaze(maze1.MazeUuid);
            await gameService.CreateNewGameWithExistingMaze(maze1.MazeUuid);
            await gameService.CreateNewGameWithExistingMaze(maze2.MazeUuid);
        }
    }
}

