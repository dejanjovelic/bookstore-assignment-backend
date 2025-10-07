using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public interface IPublisherReadService
    {
        Task<Publisher> GetByIdAsync(int id);
    }
}
