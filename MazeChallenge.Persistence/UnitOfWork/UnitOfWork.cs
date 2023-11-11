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
        private IMazeRepository _mazeRepository;
        private IBlockRepository _blockRepository;
        private IGameRepository _gameRepository;

        public UnitOfWork(MazeDbContext context)
        {
            _context = context;
        }

        public IMazeRepository MazeRepository
        {
            get
            {
                _mazeRepository ??= new MazeRepository(_context);
                return _mazeRepository;
            }
        }

        public IBlockRepository BlockRepository
        {
            get
            {
                _blockRepository ??= new BlockRepository(_context);
                return _blockRepository;
            }
        }

        public IGameRepository GameRepository
        {
            get
            {
                _gameRepository ??= new GameRepository(_context);
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

