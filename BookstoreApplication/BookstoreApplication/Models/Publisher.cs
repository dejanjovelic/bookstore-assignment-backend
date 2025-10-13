using System.ComponentModel.DataAnnotations;

namespace BookstoreApplication.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Adress { get; set; }
        [Required]
        public required string Website { get; set; }
    }
}
