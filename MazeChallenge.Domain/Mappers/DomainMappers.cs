using MazeChallenge.Domain.DTO;
using MazeChallenge.Domain.Entities;

namespace MazeChallenge.Domain.Mappers
{
    public static class DomainMappers
	{
		public static GameDto MapToGameDto(this Game game)
		{
			return new GameDto
			{
				GameUuid = game.Uuid,
				MazeUuid = game.MazeUuid,
                Completed = game.Completed,
                CurrentPositionX = game.CurrentBlock.CoordX,
                CurrentPositionY = game.CurrentBlock.CoordY,
            };
		}

        public static GameLookDto MapToGameLookDto(this Game game)
        {
            return new GameLookDto
            {
                Game = game.MapToGameDto(),
                MazeBlockView = new BlockDto
                {
                    CoordX = game.CurrentBlock.CoordX,
                    CoordY = game.CurrentBlock.CoordY,
                    NorthBlocked = game.CurrentBlock.NorthBlocked,
                    SouthBlocked = game.CurrentBlock.SouthBlocked,
                    WestBlocked = game.CurrentBlock.WestBlocked,
                    EastBlocked = game.CurrentBlock.EastBlocked
                }
            };
        }

        public static MazeDto MapToMazeDto(this Maze maze)
        {
            return new MazeDto
            {
                MazeUuid = maze.Uuid
            };
        }

        public static MazeCreatedDto MapToMazeCreatedDto(this Maze maze)
        {
            return new MazeCreatedDto
            {
                MazeUuid = maze.Uuid,
                Height = maze.Height,
                Width = maze.Width
            };
        }
    }
}

