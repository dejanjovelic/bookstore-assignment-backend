namespace BookstoreApplication.Models
{
    public interface IAwardsRepository
    {
        Task<List<Award>> GetAllAsync();
        Task<Award> GetByIdAsync(int id);
        Task<Award> CreateAsync(Award award);
        Task<Award> UpdateAsync(Award award);
        Task DeleteAsync(Award award);
    }
}
