using BookstoreApplication.Services.IServices;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookstoreApplication.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookstoreDbContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(BookstoreDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }
        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
            _transaction.Dispose();
            _transaction = null;

        }
        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
            _transaction.Dispose();
            _transaction = null;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
