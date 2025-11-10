using AutoMapper;
using BookstoreApplication.DTO;
using BookstoreApplication.Models;

namespace BookstoreApplication.Settings
{
    public class IssueProfile: Profile
    {
        public IssueProfile() 
        {
            CreateMap<CreateIssueDataDto, Issue>();
        }
    }
}
