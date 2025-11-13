using BookstoreApplication.Services.DTO;
using BookstoreApplication.Models;
using BookstoreApplication.Models.IRepositoies;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace BookstoreApplication.Infrastructure.Repositories
{
    public class BooksRepository : IBooksRepository
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

        public async Task<PaginatedListDto<Book>> GetSortedAndPaginatedBooksAsync(int sortType, int page, int pageSize)
        {
            IQueryable<Book> books = _context.Books
                .Include(book => book.Author)
                .Include(book => book.Publisher);

            books = SortBooks(books, sortType);

            int pageIndex = page - 1;
            int totalRowsCount = await books.CountAsync();
            List<Book> selectedBooks = await books.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedListDto<Book>(selectedBooks, totalRowsCount, pageIndex, pageSize);
        }

        public async Task<PaginatedListDto<Book>> GetFilteredAndSortedAndPaginatedBooksAsync(BookFilterDto filterDto, int sortType, int page, int PageSize)
        {
            IQueryable<Book> books = _context.Books
                .Include(book => book.Author)
                .Include(book => book.Publisher);

            books = FilterBooks(books, filterDto);
            books = SortBooks(books, sortType);

            int pageIndex = page - 1;
            int totalRowsCount = await books.CountAsync();
            List<Book> selectedBooks = await books.Skip(pageIndex * PageSize).Take(PageSize).ToListAsync();
            return new PaginatedListDto<Book>(selectedBooks, totalRowsCount, pageIndex, PageSize);
        }

        public static IQueryable<Book> SortBooks(IQueryable<Book> books, int sortType)
        {
            return sortType switch
            {
                (int)BookSortType.BOOK_TITLE_ASC => books.OrderBy(book => book.Title),
                (int)BookSortType.BOOK_TITLE_DESC => books.OrderByDescending(book => book.Title),
                (int)BookSortType.PUBLISHED_DATE_ASC => books.OrderBy(book => book.PublishedDate),
                (int)BookSortType.PUBLISHED_DATE_DESC => books.OrderByDescending(book => book.PublishedDate),
                (int)BookSortType.AUTHORS_FULLNAME_ASC => books.OrderBy(book => book.Author.FullName),
                (int)BookSortType.AUTHORS_FULLNAME_DESC => books.OrderByDescending(book => book.Author.FullName),
                _ => books.OrderBy(book => book.Title)
            };
        }

        public static IQueryable<Book> FilterBooks(IQueryable<Book> books, BookFilterDto filterDto)
        {
            if (!string.IsNullOrEmpty(filterDto.Title))
            {
                books = books.Where(book => (book.Title.Trim().ToLower()).Contains(filterDto.Title.Trim().ToLower()));
            }
            if (filterDto.PublishedDateFrom != null)
            {
                books = books.Where(book => book.PublishedDate >= filterDto.PublishedDateFrom);
            }
            if (filterDto.PublishedDateTo != null)
            {
                books = books.Where(book => book.PublishedDate <= filterDto.PublishedDateTo);
            }
            if (!string.IsNullOrEmpty(filterDto.AuthorFullName))
            {
                books = books.Where(book => book.Author.FullName.Trim().ToLower() == filterDto.AuthorFullName.Trim().ToLower());
            }
            if (!string.IsNullOrEmpty(filterDto.AuthorFirstName))
            {
                books = books.Where(book => book.Author.FullName.Trim().ToLower().Contains(filterDto.AuthorFirstName.Trim().ToLower()));
            }
            if (filterDto.AuthorDateOfBirthFrom != null)
            {
                books = books.Where(book => book.Author.DateOfBirth >= filterDto.AuthorDateOfBirthFrom);
            }
            if (filterDto.AuthorDateOfBirthTo != null)
            {
                books = books.Where(book => book.Author.DateOfBirth <= filterDto.AuthorDateOfBirthTo);
            }
            return books;
        }
    }
}
