using BookstoreApplication.Services.DTO;
using System.Security.Claims;

namespace BookstoreApplication.Services.IServices
{
    public interface IReviewService
    {
        Task<ReviewDto> CreateAsync(ClaimsPrincipal user, NewReviewDto newReviewDto);
        Task<List<ReviewDto>> GetAllByUserIdAsync(ClaimsPrincipal user);
    }
}