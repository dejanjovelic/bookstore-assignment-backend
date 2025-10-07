using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Threading.Tasks;

namespace BookstoreApplication.Repositories
{
    public class AwardsRepository : IAwardsRepository
    {
        private BookstoreDbContext _context;
        public AwardsRepository(BookstoreDbContext context)
        {
            this._context = context;
        }

        public async Task<List<Award>> GetAllAsync()
        {
            return await _context.Award.ToListAsync();
        }

        public async Task<Award> GetByIdAsync(int id)
        {
            return await _context.Award.FindAsync(id);
        }

        public async Task<Award> CreateAsync(Award award)
        {
                _context.Award.Add(award);
                await _context.SaveChangesAsync();
                return award;
        }

        public async Task<Award> UpdateAsync(Award award)
        {
            _context.Update(award);
            await _context.SaveChangesAsync();
            return award;
        }

        public async Task DeleteAsync(Award award)
        {
            _context.Award.Remove(award);
            await _context.SaveChangesAsync();
        }
    }
}
