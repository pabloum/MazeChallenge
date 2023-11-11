using System;
namespace MazeChallenge.Domain.Entities
{
	public class Game
	{
		public Guid GameUuid { get; set; }
		public Guid MazeUuid { get; set; }

        public int CurrentPositionX { get; set; }
        public int CurrentPositionY { get; set; }

		public bool Completed { get; set; }
	}
}

