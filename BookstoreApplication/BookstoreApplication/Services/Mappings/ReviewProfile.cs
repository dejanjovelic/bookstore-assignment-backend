using AutoMapper;
using BookstoreApplication.Models;
using BookstoreApplication.Services.DTO;

namespace BookstoreApplication.Services.Mappings
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile() 
        {
            CreateMap<NewReviewDto, Review>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ApplicationUser, ApplicationUser>();

        }
    }
}
