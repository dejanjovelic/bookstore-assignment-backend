using BookstoreApplication.DTO;
using BookstoreApplication.Models;
using BookstoreApplication.Models.IRepositoies;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class PublishersRepository:IPublishersRepository
    {
        private BookstoreDbContext _context;

        public PublishersRepository(BookstoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Publisher>> GetAllAsync()
        {
            return await _context.Publishers.ToListAsync();
        }

        public async Task<Publisher> GetByIdAsync(int id)
        {
            return await _context.Publishers.FindAsync(id);
        }

        public async Task<Publisher> CreateAsync(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();
            return publisher;
        }
        public async Task<Publisher> UpdateAsync(Publisher publisher)
        {
            await _context.SaveChangesAsync();
            return publisher;
        }

        public async Task DeleteAsync(Publisher publisher)
        {
            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync(true);
        }
        public async Task<IEnumerable<Publisher>> GetSortedPublishers(int sortType)
        {
            IQueryable<Publisher> publishers = _context.Publishers;

            publishers = SortPublishers(publishers, sortType);
            return await publishers.ToListAsync();
        }

        private static IQueryable<Publisher> SortPublishers(IQueryable<Publisher> publishers, int sortType)
        {
            return sortType switch
            {
                (int)PublisherSortType.NAME_ASCENDING => publishers.OrderBy(publisher => publisher.Name),
                (int)PublisherSortType.NAME_DESCENDING => publishers.OrderByDescending(publisher => publisher.Name),
                (int)PublisherSortType.ADDRESS_ASCENDING => publishers.OrderBy(publisher=>publisher.Adress),
                (int)PublisherSortType.ADDRESS_DESCENDING=>publishers.OrderByDescending(publisher=>publisher.Adress),
                _=>publishers.OrderBy(publisher => publisher.Name),
            };
        }
    }
}
