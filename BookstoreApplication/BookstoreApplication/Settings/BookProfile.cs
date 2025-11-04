using AutoMapper;
using BookstoreApplication.DTO;
using BookstoreApplication.Models;
using BookstoreApplication.Services;

namespace BookstoreApplication.Settings
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(
                dest => dest.YearsSincePublication,
                opt => opt.MapFrom(src => DateTime.Today.Year - src.PublishedDate.Year));
            CreateMap<Book, BookDetailsDto>();
            CreateMap<RegistrationDto, ApplicationUser>();
            CreateMap<ApplicationUser, ProfileDto>();
        }
    }
}
