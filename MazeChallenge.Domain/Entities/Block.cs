using System;
namespace MazeChallenge.Domain.Entities
{
	public class Block
	{
        public Guid BlockUuid { get; set; }

        public Maze Maze { get; set; }
        public Guid MazeUuid { get; set; }

        public int CoordX { get; set; }
        public int CoordY { get; set; }

        public bool NorthBlocked { get; set; }
        public bool SouthBlocked { get; set; }
        public bool WestBlocked { get; set; }
        public bool EastBlocked { get; set; }
    }
}

