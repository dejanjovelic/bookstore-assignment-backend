using AutoMapper;
using BookstoreApplication.Models;
using BookstoreApplication.Services.DTO;

namespace BookstoreApplication.Services.Mappings
{
    public class IssueProfile: Profile
    {
        public IssueProfile() 
        {
            CreateMap<CreateIssueDataDto, Issue>();
        }
    }
}
