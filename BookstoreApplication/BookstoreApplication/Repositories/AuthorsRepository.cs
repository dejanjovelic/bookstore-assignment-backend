using BookstoreApplication.DTO;
using BookstoreApplication.Models;
using BookstoreApplication.Models.IRepositoies;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class AuthorsRepository : IAuthorsRepository
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
        public async Task<PaginatedListDto<Author>> GetAllAuthorsPaginatedAsync(int page, int pageSize) 
        {

            IQueryable<Author> authors = _context.Authors
                .OrderBy(author => author.FullName);

            int pageIndex = page - 1;
            int totalRowCount = await _context.Authors.CountAsync();
            List<Author> selectedAuthors = await authors.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            PaginatedListDto<Author> result = new PaginatedListDto<Author>(selectedAuthors, totalRowCount, pageIndex, pageSize);
            return result;
        }

    }
}
