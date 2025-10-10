using BookstoreApplication.DTO;
using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public interface IAuthorService : IAuthorReadService
    {
        Task<List<Author>> GetAllAsync();
        Task<Author> CreateAsync(Author author);
        Task<Author> UpdateAsync(int id, Author author);
        Task DeleteAsync(int id);
        Task<PaginatedListDto<Author>> GetAllAuthorsPaginatedAsync(int page, int pageSize);
    }
}
