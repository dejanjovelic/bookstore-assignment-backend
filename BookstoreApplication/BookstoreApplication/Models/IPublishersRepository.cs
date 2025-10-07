namespace BookstoreApplication.Models
{
    public interface IPublishersRepository
    {
        Task<List<Publisher>> GetAllAsync();
        Task<Publisher> GetByIdAsync(int id);
        Task<Publisher> CreateAsync(Publisher publisher);
        Task<Publisher> UpdateAsync(Publisher publisher);
        Task DeleteAsync(Publisher publisher);
    }
}
