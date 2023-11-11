using System;
using System.ComponentModel.DataAnnotations;

namespace MazeChallenge.Domain.Entities
{
	public class BaseEntity
    {
        [Key]
        public Guid Uuid { get; set; }
    }
}

