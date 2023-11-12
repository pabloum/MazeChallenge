using System;
namespace MazeChallenge.Domain.DTO
{
    public class GameDto
	{
        public Guid GameUuid { get; set; }
        public Guid MazeUuid { get; set; }
        public bool Completed { get; set; }
        public int CurrentPositionX { get; set; }
        public int CurrentPositionY { get; set; }
    }
}

