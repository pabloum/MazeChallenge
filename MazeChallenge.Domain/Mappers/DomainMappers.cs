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
				MazeUuid = game.MazeUuid
			};
		}

        public static MazeDto MapToMazeDto(this Maze maze)
        {
            return new MazeDto
            {
                MazeUuid = maze.Uuid
            };
        }
    }
}

