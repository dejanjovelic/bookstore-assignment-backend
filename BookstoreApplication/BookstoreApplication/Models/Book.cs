using System.ComponentModel.DataAnnotations;

namespace BookstoreApplication.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public required string Title { get; set; }
        [Required]
        public int PageCount { get; set; }
        [Required]
        public DateTime PublishedDate { get; set; }
        [Required]
        public required string ISBN { get; set; }

        [Required]
        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        [Required]
        public int PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
    }
}
