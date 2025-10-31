namespace BookstoreApplication.DTO
{
    public class BookFilterDto
    {
        public string? Title { get; set; }
        public DateTime? PublishedDateFrom { get; set; }
        public DateTime? PublishedDateTo { get; set; }
        public string? AuthorFullName { get; set; }
        public string? AuthorFirstName { get; set; }
        public DateTime? AuthorDateOfBirthFrom { get; set; }
        public DateTime? AuthorDateOfBirthTo { get; set; }
    }
}

