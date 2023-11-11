using MazeChallenge.Domain.Context;
using MazeChallenge.Domain.Entities;
using MazeChallenge.Persistence.Contracts;
using MazeChallenge.Persistence.Implementations;
using Microsoft.EntityFrameworkCore;

namespace MazeChallenge.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MazeDbContext _context;
        private bool _disposed = false;
        private IBaseRepository<Maze> _mazeRepository;
        private IBaseRepository<Block> _blockRepository;
        private IBaseRepository<Game> _gameRepository;

        public UnitOfWork(MazeDbContext context)
        {
            _context = context;
        }

        public IBaseRepository<Maze> MazeRepository
        {
            get
            {
                _mazeRepository ??= new BaseRepository<Maze>(_context);
                return _mazeRepository;
            }
        }

        public IBaseRepository<Block> BlockRepository
        {
            get
            {
                _blockRepository ??= new BaseRepository<Block>(_context);
                return _blockRepository;
            }
        }

        public IBaseRepository<Game> GameRepository
        {
            get
            {
                _gameRepository ??= new BaseRepository<Game>(_context);
                return _gameRepository;
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
            }
        }
        #region IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.DisposeAsync();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}

