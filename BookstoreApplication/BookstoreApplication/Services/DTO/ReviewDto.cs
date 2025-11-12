using BookstoreApplication.Models;

namespace BookstoreApplication.Services.DTO
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public int BookId { get; set; }
        public BookDto? Book { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
