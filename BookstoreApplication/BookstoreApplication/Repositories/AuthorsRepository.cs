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
            try
            {
                return await _context.Authors.ToListAsync();

    }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
            ;
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Authors.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<Author> CreateAsync(Author author)
        {
            try
            {
                _context.Authors.Add(author);
                await _context.SaveChangesAsync();
                return author;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<Author> UpdateAsync(Author author)
        {
            try
            {
                await _context.SaveChangesAsync();
                return author;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Author author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    return false;
                }
                _context.Remove(author);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

    }
}
