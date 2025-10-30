using BookstoreApplication.DTO;

namespace BookstoreApplication.Models.IRepositoies
{
    public interface IPublishersRepository
    {
        Task<List<Publisher>> GetAllAsync();
        Task<Publisher> GetByIdAsync(int id);
        Task<Publisher> CreateAsync(Publisher publisher);
        Task<Publisher> UpdateAsync(Publisher publisher);
        Task DeleteAsync(Publisher publisher);
        Task<IEnumerable<Publisher>> GetSortedPublishers(int sortType);
    }
}
