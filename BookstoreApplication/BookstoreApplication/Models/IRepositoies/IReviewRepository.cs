
using System.Security.Claims;

namespace BookstoreApplication.Models.IRepositoies
{
    public interface IReviewRepository
    {
        Task<bool> CheckDuplicateReviewAsync(string userId, int bookId);
        Task<List<Review>> GetAllbyBookIdAsync(int bookId);
        void Create(Review newReview);
        Task<List<Review>> GetAllByUserIdAsync(string userId);
    }
}
