using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using MazeChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MazeChallenge.Domain.Context
{
	public class MazeDbContext : DbContext, IDisposable
    {
        public MazeDbContext(DbContextOptions<MazeDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Maze> Mazes { get; set; }
        public virtual DbSet<Block> Blocks { get; set; }
        public virtual DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Maze>(entity =>
            {
                entity.HasKey(r => r.Uuid);
                entity.ToTable("Mazes");
            });

            modelBuilder.Entity<Block>(entity =>
            {
                entity.HasKey(b => b.Uuid);
                entity.HasOne(b => b.Maze).WithMany(m => m.Blocks).HasForeignKey(b => b.MazeUuid);
                entity.ToTable("Blocks");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(r => r.Uuid);
                entity.ToTable("Games");
            });
        }
    }
}

