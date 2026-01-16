using BookstoreApplication.Models;
using BookstoreApplication.Services.DTO;

namespace BookstoreApplication.Services.IServices
{
    public interface IIssueService
    {
        Task<Issue> CreateAsync(CreateIssueDataDto issueDataDto);
        Task<List<IssueDto>> GetIssuesFromVolumeAsync(int volumeId);
    }
}
