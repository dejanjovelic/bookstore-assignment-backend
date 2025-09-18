using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class PublishersRepository
    {
        private BookstoreDbContext _context;

        public PublishersRepository(BookstoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Publisher>> GetAll()
        {
            try
            {
                return await _context.Publishers.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<Publisher> GetById(int id)
        {
            try
            {
                return await _context.Publishers.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<Publisher> Create(Publisher publisher)
        {
            try
            {
                _context.Publishers.Add(publisher);
                await _context.SaveChangesAsync();
                return publisher;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        public async Task<Publisher> Update(Publisher publisher)
        {
            try
            {
                await _context.SaveChangesAsync();
                return publisher;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                Publisher publisher = _context.Publishers.Find(id);
                if (publisher != null)
                {
                    return false;
                }
                _context.Publishers.Remove(publisher);
                await _context.SaveChangesAsync(true);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}
