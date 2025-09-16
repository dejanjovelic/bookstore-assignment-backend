namespace BookstoreApplication.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Adress { get; set; }
        public required string Website { get; set; }
    }
}
