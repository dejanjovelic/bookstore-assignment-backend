using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public interface IAuthorReadService
    {
        Task<Author> GetByIdAsync(int id);
    }
}
