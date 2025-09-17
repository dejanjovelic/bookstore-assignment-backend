namespace BookstoreApplication.Models
{
    public class Award
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AwardStartYear { get; set; }
        public List<AuthorAwardRecord> AuthorAwards { get; set; } = new List<AuthorAwardRecord>();
    }
}
