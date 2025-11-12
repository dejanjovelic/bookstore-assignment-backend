namespace BookstoreApplication.Services.DTO
{
    public class BookDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string ISBN { get; set; }
        public string AuthorFullName { get; set; }
        public string PublisherName {  get; set; }
        public int YearsSincePublication { get; set; }

    }
}