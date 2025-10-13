using System.ComponentModel.DataAnnotations;

namespace BookstoreApplication.Models
{
    public class Award
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime AwardStartYear { get; set; }
        public List<AuthorAwardRecord> AuthorAwards { get; set; } = new List<AuthorAwardRecord>();
    }
}
