namespace BookstoreApplication.Models
{
    public class AuthorAwardRecord
    {
        public int Id { get; set; }
        public DateTime AwardedOn { get; set; }

        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        public int AwardId { get; set; }
        public Award? Award { get; set; }

    }
}
