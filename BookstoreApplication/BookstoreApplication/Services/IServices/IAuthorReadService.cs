using BookstoreApplication.Models;

namespace BookstoreApplication.Services.IServices
{
    public interface IAuthorReadService
    {
        Task<Author> GetByIdAsync(int id);
    }
}
