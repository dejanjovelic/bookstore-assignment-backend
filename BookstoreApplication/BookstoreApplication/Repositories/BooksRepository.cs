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
            return await _context.Books
                    .Include(book => book.Author)
                    .Include(book => book.Publisher)
                    .ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(book => book.Author)
                .Include(book => book.Publisher)
                .FirstOrDefaultAsync(book => book.Id == id);
        }


        public async Task<Book> CreateAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task DeleteAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

    }
}
