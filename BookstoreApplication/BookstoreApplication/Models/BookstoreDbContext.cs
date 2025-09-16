using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Models
{
    public class BookstoreDbContext : DbContext
    {
        public BookstoreDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        protected BookstoreDbContext()
        {
        }
    }
}
