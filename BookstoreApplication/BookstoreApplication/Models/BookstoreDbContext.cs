using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Models
{
    public class BookstoreDbContext : DbContext
    {
        public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options) : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Award> Award { get; set; }
        public DbSet<AuthorAwardRecord> AuthorAwardRecord { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorAwardRecord>(entity => {
                entity.ToTable("AuthorAwardBridge");
                entity.HasOne(record=>record.Author)
                .WithMany(author=>author.AuthorAwards)
                .HasForeignKey(record=>record.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(author=>author.DateOfBirth).HasColumnName("Bithday");
            });

        }

       
    }
}
