using BookstoreApplication.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using BookstoreApplication.Models.IRepositoies;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Infrastructure
{
    public class IssuesRepository : IIssuesRepository
    {
        private readonly BookstoreDbContext _context;

        public IssuesRepository(BookstoreDbContext context)
        {
            _context = context;
        }
        public async Task<Issue> GetByExternalId(int externalId) 
        {
            return await _context.Issues
                .FirstOrDefaultAsync(issue => issue.ExternalId == externalId);
        }

        public async Task<Issue> CreateAsync(Issue newIssue)
        {
            _context.Issues.Add(newIssue);
            await _context.SaveChangesAsync();
            return newIssue;
        }
    }
}
