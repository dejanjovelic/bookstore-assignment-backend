using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class AuthorsRepository
    {
        private BookstoreDbContext _context;

        public AuthorsRepository(BookstoreDbContext context)
        {
            this._context = context;
        }

        public async Task<List<Author>> GetAllAsync()
        {
                return await _context.Authors.ToListAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
                return await _context.Authors.FindAsync(id);
        }

        public async Task<Author> CreateAsync(Author author)
        {
            
                _context.Authors.Add(author);
                await _context.SaveChangesAsync();
                return author;
        }

        public async Task<Author> UpdateAsync(Author author)
        {
            
                await _context.SaveChangesAsync();
                return author;
        }

        public async Task DeleteAsync(Author author)
        {
                _context.Remove(author);
                await _context.SaveChangesAsync();
        }

    }
}
