using AutoMapper;
using BookstoreApplication.Models;
using BookstoreApplication.Services.DTO;

namespace BookstoreApplication.Services.Mappings
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
