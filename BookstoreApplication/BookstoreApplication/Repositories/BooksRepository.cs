using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class BooksRepository
    {
        private BookstoreDbContext _context;

        public BooksRepository(BookstoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllAsync() 
        {
            try
            {
                return await _context.Books
                        .Include(book => book.Author)
                        .Include(book => book.Publisher)
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<Book> GetByIdAsync(int id) 
        {
            try
            {
                return await _context.Books
                    .Include(book => book.Author)
                    .Include(book => book.Publisher)
                    .FirstOrDefaultAsync(book => book.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        public async Task<Book> CreateAsync(Book book) 
        {
            try
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                return book;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }        
        }

        public async Task<Book> UpdateAsync(Book book) 
        {
            try
            {
                await _context.SaveChangesAsync();
                return book;
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
                Book book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    return false;
                }
                _context.Books.Remove(book);
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
