﻿using AutoMapper;
using BookstoreApplication.DTO;
using BookstoreApplication.Models;

namespace BookstoreApplication.Settings
{
    public class BookProfile : Profile
    {
        public BookProfile() 
        {
            CreateMap<Book, BookDto>()
                .ForMember(
                dest=> dest.YearsSincePublication,
                opt=>opt.MapFrom(src=>DateTime.Today.Year-src.PublishedDate.Year));
            CreateMap<Book, BookDetailsDto>();
        }
    }
}
