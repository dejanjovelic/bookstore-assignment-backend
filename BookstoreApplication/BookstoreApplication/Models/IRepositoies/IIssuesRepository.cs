namespace BookstoreApplication.Models.IRepositoies
{
    public interface IIssuesRepository
    {
        Task<Issue> GetByExternalId(int externalId);
        Task<Issue> CreateAsync(Issue newIssue);

    }
}
