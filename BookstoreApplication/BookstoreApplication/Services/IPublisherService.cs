using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public interface IPublisherService : IPublisherReadService
    {
        Task<List<Publisher>> GetAllAsync();
        Task<Publisher> CreateAsync(Publisher publisher);
        Task<Publisher> UpdateAsync(int id, Publisher publisher);
        Task DeleteAsync(int id);
    }
}
