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

        public async Task<List<Book>> GetAll() 
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

        public async Task<Book> GetById(int id) 
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


        public async Task<Book> Create(Book book) 
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

        public async Task<Book> Update(Book book) 
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

        public async Task<bool> Delete(int id) 
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
