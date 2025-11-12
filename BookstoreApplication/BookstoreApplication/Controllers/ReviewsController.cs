using BookstoreApplication.Services.DTO;
using BookstoreApplication.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] NewReviewDto newReviewDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _reviewService.CreateAsync(User, newReviewDto));
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetAllByUserIdAsync() 
        {
            return Ok(await _reviewService.GetAllByUserIdAsync(User));
        }
    }
}
