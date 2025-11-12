using AutoMapper;
using BookstoreApplication.Models;
using BookstoreApplication.Models.IRepositoies;
using BookstoreApplication.Services.DTO;
using BookstoreApplication.Services.Exceptions;
using BookstoreApplication.Services.IServices;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Collections.Generic;
using System.Security.Claims;

namespace BookstoreApplication.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IBooksRepository _booksRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IBooksRepository booksRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _booksRepository = booksRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReviewDto> CreateAsync(ClaimsPrincipal user, NewReviewDto newReviewDto)
        {
            Book book = await _booksRepository.GetByIdAsync(newReviewDto.BookId);
            if (book == null)
            {
                throw new NotFoundException($"Book with Id: {newReviewDto.BookId} not found.");
            }

            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            bool isDuplicate = await _reviewRepository.CheckDuplicateReviewAsync(userId, newReviewDto.BookId);
            if (isDuplicate)
            {
                throw new ConflictException("User has already reviewed this book.");
            }

            Review newReview = _mapper.Map<Review>(newReviewDto);
            newReview.UserId = userId;

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _reviewRepository.Create(newReview);
                await _unitOfWork.SaveAsync();
                double averageRating = (await _reviewRepository.GetAllbyBookIdAsync(newReview.BookId)).Average(review => review.Rating);
                book.AverageRating = averageRating;

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
            return _mapper.Map<ReviewDto>(newReview);

        }

        public async Task<List<ReviewDto>> GetAllByUserIdAsync(ClaimsPrincipal user)
        {
            string userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var reviews = await _reviewRepository.GetAllByUserIdAsync(userId);
            return reviews.Select(review => _mapper.Map<ReviewDto>(review)).ToList();
        }
    }
}
