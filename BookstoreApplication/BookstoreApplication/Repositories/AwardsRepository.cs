using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Threading.Tasks;

namespace BookstoreApplication.Repositories
{
    public class AwardsRepository
    {
        private BookstoreDbContext _context;
        public AwardsRepository(BookstoreDbContext context)
        {
            this._context = context;
        }

        public async Task<List<Award>> GetAllAsync()
        {
            try
            {
                return await _context.Award.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<Award> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Award.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<Award> CreateAsync(Award award)
        {
            try
            {
                _context.Award.Add(award);
                await _context.SaveChangesAsync();
                return award;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }

        }

        public async Task<Award> UpdateAsync(Award award)
        {
            try
            {
                _context.Update(award);
                await _context.SaveChangesAsync();
                return award;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Award award = await _context.Award.FindAsync(id);
                if (award == null)
                {
                    return false;
                }

                _context.Award.Remove(award);
                await _context.SaveChangesAsync();
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
