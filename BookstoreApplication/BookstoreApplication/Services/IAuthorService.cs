using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public interface IAuthorService : IAuthorReadService
    {
        Task<List<Author>> GetAllAsync();
        Task<Author> CreateAsync(Author author);
        Task<Author> UpdateAsync(Author author);
        Task DeleteAsync(int id);
    }
}
