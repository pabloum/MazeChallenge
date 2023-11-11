using System;
namespace MazeChallenge.Domain.Entities
{
	public class Maze
	{
        public Guid MazeUuid { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public IEnumerable<Block> Blocks { get; set; }
    }
}

