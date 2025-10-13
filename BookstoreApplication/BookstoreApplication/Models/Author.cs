using System.ComponentModel.DataAnnotations;

namespace BookstoreApplication.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        public required string FullName { get; set; }
        [Required]
        public required string Biography { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }

        public List<AuthorAwardRecord> AuthorAwards { get; set; } = new List<AuthorAwardRecord>();
    }
}
