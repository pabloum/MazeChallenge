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
            var maze1 = await mazeService.CreateNewMaze(5, 5);
            var maze2 = await mazeService.CreateNewMaze(15, 14);
            var maze3 = await mazeService.CreateNewMaze(10, 10);

            await gameService.CreateNewGameWithExistingMaze(maze1.MazeUuid);
            await gameService.CreateNewGameWithExistingMaze(maze1.MazeUuid);
            await gameService.CreateNewGameWithExistingMaze(maze2.MazeUuid);
            await gameService.CreateNewGameWithExistingMaze(maze3.MazeUuid);
        }
    }
}

