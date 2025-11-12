namespace BookstoreApplication.Services.DTO
{
    public class CreateIssueDataDto
    {
        public string Name { get; set; }
        public DateTime CoverDate { get; set; }
        public string IssueNumber { get; set; }
        public string ImageUrl { get; set; }
        public string? Description { get; set; }
        public int ExternalId { get; set; }
        public int NumberOfPages { get; set; }
        public double Price { get; set; }
        public int AvailableCopies { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
