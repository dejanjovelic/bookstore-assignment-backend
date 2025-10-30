using BookstoreApplication.DTO;
using BookstoreApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Services.IServices
{
    public interface IPublisherService : IPublisherReadService
    {
        Task<List<Publisher>> GetAllAsync();
        Task<Publisher> CreateAsync(Publisher publisher);
        Task<Publisher> UpdateAsync(int id, Publisher publisher);
        Task DeleteAsync(int id);
        List<PublisherSortTypeOptionDto> GetAllSortTypes();
        Task<IEnumerable<Publisher>> GetSortedPublishers(int sortType);
    }
}
