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

        public async Task<List<Author>> GetAll()
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

        public async Task<Author> GetById(int id)
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

        public async Task<Author> Create(Author author)
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

        public async Task<Author> Update(Author author)
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

        public async Task<bool> Delete(int id)
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
