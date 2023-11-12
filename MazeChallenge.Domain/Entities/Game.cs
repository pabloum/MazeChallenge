using System;
namespace MazeChallenge.Domain.Entities
{
	public class Game : BaseEntity
    {
		public Guid MazeUuid { get; set; }
		public Guid CurrentBlockUuid { get; set; }
		public Block CurrentBlock { get; set; }
		public bool Completed { get; set; }
	}
}

