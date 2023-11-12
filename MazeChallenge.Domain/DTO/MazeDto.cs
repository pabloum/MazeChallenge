using System;
namespace MazeChallenge.Domain.DTO
{
    public class MazeDto
	{
        public Guid MazeUuid { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public IEnumerable<BlockDto> Blocks { get; set; }
    }
}

