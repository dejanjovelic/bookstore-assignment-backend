using BookstoreApplication.DTO;

namespace BookstoreApplication.Services.IServices
{
    public interface IIssueService
    {
        Task<object?> CreateAsync(CreateIssueDataDto issueDataDto);
        Task<List<IssueDto>> GetIssuesFromVolumeAsync(int volumeId);
    }
}
