using BookstoreApplication.Models;
using BookstoreApplication.Models.IRepositoies;
using Microsoft.EntityFrameworkCore;
namespace BookstoreApplication.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly BookstoreDbContext _context;

        public ReviewRepository(BookstoreDbContext context)
        {
            _context = context;
        }

        public void Create(Review newReview)
        {
            _context.Reviews.Add(newReview);
        }

        public async Task<bool> CheckDuplicateReviewAsync(string userId, int bookId)
        {
            return await _context.Reviews.AnyAsync(review => review.UserId == userId && review.BookId == bookId);
        }

        public async Task<List<Review>> GetAllbyBookIdAsync(int bookId)
        {
            return await _context.Reviews
                .Where(review => review.BookId == bookId)
                .ToListAsync();
        }

        public async Task<List<Review>> GetAllByUserIdAsync(string userId)
        {
            return await _context.Reviews
                .Where(review => review.UserId == userId)
                .ToListAsync();
        }

    }
}
