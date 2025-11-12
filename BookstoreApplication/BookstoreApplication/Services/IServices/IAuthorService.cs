using BookstoreApplication.Models;
using BookstoreApplication.Services.DTO;

namespace BookstoreApplication.Services.IServices
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
