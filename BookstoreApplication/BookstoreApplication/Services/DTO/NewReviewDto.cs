using System.ComponentModel.DataAnnotations;

namespace BookstoreApplication.Services.DTO
{
    public class NewReviewDto
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        public string? Comment { get; set; }
    }
}
