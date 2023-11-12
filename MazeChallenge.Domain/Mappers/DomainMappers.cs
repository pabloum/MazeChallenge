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
                MazeBlockView = game.CurrentBlock.MapToBlockDto()
            };
        }

        public static MazeDto MapToMazeDto(this Maze maze)
        {
            return new MazeDto
            {
                MazeUuid = maze.Uuid,
                Height = maze.Height,
                Width = maze.Width,
                Blocks = maze.Blocks.Select(b => b.MapToBlockDto())

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

        public static BlockDto MapToBlockDto(this Block block)
        {
            return new BlockDto
            {
                CoordX = block.CoordX,
                CoordY = block.CoordY,
                NorthBlocked = block.NorthBlocked,
                SouthBlocked = block.SouthBlocked,
                WestBlocked = block.WestBlocked,
                EastBlocked = block.EastBlocked,
            };
        }
    }
}

