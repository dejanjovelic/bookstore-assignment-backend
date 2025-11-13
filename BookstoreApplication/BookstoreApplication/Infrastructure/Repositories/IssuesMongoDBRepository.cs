using BookstoreApplication.Models;
using BookstoreApplication.Models.IRepositoies;
using MongoDB.Driver;

namespace BookstoreApplication.Infrastructure.Repositories
{
    public class IssuesMongoDBRepository : IIssuesRepository
    {
        private readonly IMongoCollection<Issue> _issuesCollection;

        public IssuesMongoDBRepository(IConfiguration configuration)
        {
            var client = new MongoClient( configuration["MongoDB:ConnectionString"]);
            var database = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
            _issuesCollection = database.GetCollection<Issue>(configuration["MongoDB:CollectionName"]);
        }
        public async Task<Issue> GetByExternalId(int externalId)
        {
            return await _issuesCollection
                .Find(issue => issue.ExternalId == externalId).FirstOrDefaultAsync();
        }

        public async Task<Issue> CreateAsync(Issue newIssue)
        {
           await _issuesCollection.InsertOneAsync(newIssue);
            
            return newIssue;
        }
    }
}
