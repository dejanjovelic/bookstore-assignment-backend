using BookstoreApplication.Models;

namespace BookstoreApplication.DTO
{
    public class BookDetailsDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishedDate { get; set; }
        public required string ISBN { get; set; }
        public int AuthorId { get; set; }
        public string AuthorFullName { get; set; }
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
    }
}
