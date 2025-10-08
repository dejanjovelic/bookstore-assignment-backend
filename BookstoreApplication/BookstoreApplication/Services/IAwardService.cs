using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public interface IAwardService
    {
        Task<List<Award>> GetAllAsync();
        Task<Award> GetByIdAsync(int id);
        Task<Award> CreateAsync(Award award);
        Task<Award> UpdateAsync(int id, Award award);
        Task DeleteAsync(int id);
    }
}
