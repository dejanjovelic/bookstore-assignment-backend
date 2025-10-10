using BookstoreApplication.DTO;

namespace BookstoreApplication.Models
{
    public interface IAuthorsRepository
    {
        Task<List<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(int id);
        Task<Author> CreateAsync(Author author);
        Task<Author> UpdateAsync(Author author);
        Task DeleteAsync(Author author);
        Task<PaginatedListDto<Author>> GetAllAuthorsPaginatedAsync(int page, int pageSize);
    }
}
