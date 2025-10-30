using BookstoreApplication.Models;

namespace BookstoreApplication.Services.IServices
{
    public interface IPublisherReadService
    {
        Task<Publisher> GetByIdAsync(int id);
    }
}
