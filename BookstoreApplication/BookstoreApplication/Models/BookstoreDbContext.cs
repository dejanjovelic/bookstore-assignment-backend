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
            modelBuilder.Entity<AuthorAwardRecord>(entity =>
            {
                entity.ToTable("AuthorAwardBridge");

                entity.HasOne(record => record.Author)
                .WithMany(author => author.AuthorAwards)
                .HasForeignKey(record => record.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(record => record.Award)
                .WithMany(award => award.AuthorAwards)
                .HasForeignKey(record => record.AwardId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(author => author.DateOfBirth).HasColumnName("Birthday");
            });

            modelBuilder.Entity<Book>()
                .HasOne(book => book.Publisher)
                .WithMany()
                .HasForeignKey(book => book.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Rivers of Time", PageCount = 320, PublishedDate = new DateTime(2010, 5, 1, 0, 0, 0, DateTimeKind.Utc), ISBN = "978000000001", AuthorId = 1, PublisherId = 1 },
                new Book { Id = 2, Title = "The Hidden Valley", PageCount = 280, PublishedDate = new DateTime(2011, 9, 10, 0, 0, 0, DateTimeKind.Utc), ISBN = "978000000002", AuthorId = 1, PublisherId = 2 },
                new Book { Id = 3, Title = "Magic Adventures", PageCount = 150, PublishedDate = new DateTime(2015, 3, 15, 0, 0, 0, DateTimeKind.Utc), ISBN = "978000000003", AuthorId = 2, PublisherId = 1 },
                new Book { Id = 4, Title = "Forest Tales", PageCount = 180, PublishedDate = new DateTime(2018, 8, 20, 0, 0, 0, DateTimeKind.Utc), ISBN = "978000000004", AuthorId = 2, PublisherId = 3 },
                new Book { Id = 5, Title = "Empire Shadows", PageCount = 410, PublishedDate = new DateTime(2012, 6, 30, 0, 0, 0, DateTimeKind.Utc), ISBN = "978000000005", AuthorId = 3, PublisherId = 2 },
                new Book { Id = 6, Title = "Lost Crown", PageCount = 390, PublishedDate = new DateTime(2014, 11, 12, 0, 0, 0, DateTimeKind.Utc), ISBN = "978000000006", AuthorId = 3, PublisherId = 3 },
                new Book { Id = 7, Title = "Hearts and Stars", PageCount = 250, PublishedDate = new DateTime(2019, 2, 5, 0, 0, 0, DateTimeKind.Utc), ISBN = "978000000007", AuthorId = 4, PublisherId = 1 },
                new Book { Id = 8, Title = "Summer Letters", PageCount = 230, PublishedDate = new DateTime(2020, 7, 14, 0, 0, 0, DateTimeKind.Utc), ISBN = "978000000008", AuthorId = 4, PublisherId = 2 },
                new Book { Id = 9, Title = "Cosmic Voyage", PageCount = 500, PublishedDate = new DateTime(2016, 4, 22, 0, 0, 0, DateTimeKind.Utc), ISBN = "978000000009", AuthorId = 5, PublisherId = 3 },
                new Book { Id = 10, Title = "Beyond the Stars", PageCount = 450, PublishedDate = new DateTime(2018, 9, 18, 0, 0, 0, DateTimeKind.Utc), ISBN = "978000000010", AuthorId = 5, PublisherId = 1 },
                new Book { Id = 11, Title = "Parallel Worlds", PageCount = 470, PublishedDate = new DateTime(2021, 1, 10, 0, 0, 0, DateTimeKind.Utc), ISBN = "978000000011", AuthorId = 5, PublisherId = 2 },
                new Book { Id = 12, Title = "Time Horizons", PageCount = 430, PublishedDate = new DateTime(2022, 12, 3, 0, 0, 0, DateTimeKind.Utc), ISBN = "978000000012", AuthorId = 1, PublisherId = 3 }
            );

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, FullName = "John Smith", Biography = "Pisac romana i eseja.", DateOfBirth = new DateTime(1975, 5, 12, 0, 0, 0, DateTimeKind.Utc) },
                new Author { Id = 2, FullName = "Emily Johnson", Biography = "Autorka dečjih knjiga.", DateOfBirth = new DateTime(1982, 8, 24, 0, 0, 0, DateTimeKind.Utc) },
                new Author { Id = 3, FullName = "Michael Brown", Biography = "Specijalista za istorijske trilere.", DateOfBirth = new DateTime(1968, 1, 3, 0, 0, 0, DateTimeKind.Utc) },
                new Author { Id = 4, FullName = "Sophia Davis", Biography = "Piše savremene ljubavne romane.", DateOfBirth = new DateTime(1990, 10, 14, 0, 0, 0, DateTimeKind.Utc) },
                new Author { Id = 5, FullName = "David Wilson", Biography = "Poznat po naučno-fantastičnim delima.", DateOfBirth = new DateTime(1978, 3, 30, 0, 0, 0, DateTimeKind.Utc) }
            );

            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { Id = 1, Name = "Sunrise Books", Adress = "Main Street 12", Website = "https://sunrisebooks.com" },
                new Publisher { Id = 2, Name = "Blue Ocean Press", Adress = "Harbor Ave 45", Website = "https://blueoceanpress.com" },
                new Publisher { Id = 3, Name = "Galaxy Publishing", Adress = "Cosmos Blvd 99", Website = "https://galaxypublishing.com" }
            );

            modelBuilder.Entity<Award>().HasData(
                new Award { Id = 1, Name = "National Book Prize", Description = "Najbolja knjiga godine", AwardStartYear = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Award { Id = 2, Name = "Children’s Literature Award", Description = "Najbolje dečje štivo", AwardStartYear = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Award { Id = 3, Name = "Historical Fiction Medal", Description = "Najbolji istorijski roman", AwardStartYear = new DateTime(1998, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Award { Id = 4, Name = "Sci-Fi Galaxy Award", Description = "Najbolja SF knjiga", AwardStartYear = new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
                );

            modelBuilder.Entity<AuthorAwardRecord>().HasData(
            new AuthorAwardRecord { Id = 1, AuthorId = 1, AwardId = 1, AwardedOn = new DateTime(2011, 5, 1, 0, 0, 0, DateTimeKind.Utc) },
            new AuthorAwardRecord { Id = 2, AuthorId = 1, AwardId = 3, AwardedOn = new DateTime(2015, 7, 12, 0, 0, 0, DateTimeKind.Utc) },
            new AuthorAwardRecord { Id = 3, AuthorId = 2, AwardId = 2, AwardedOn = new DateTime(2016, 2, 20, 0, 0, 0, DateTimeKind.Utc) },
            new AuthorAwardRecord { Id = 4, AuthorId = 2, AwardId = 1, AwardedOn = new DateTime(2018, 3, 5, 0, 0, 0, DateTimeKind.Utc) },
            new AuthorAwardRecord { Id = 5, AuthorId = 3, AwardId = 3, AwardedOn = new DateTime(2013, 10, 8, 0, 0, 0, DateTimeKind.Utc) },
            new AuthorAwardRecord { Id = 6, AuthorId = 3, AwardId = 1, AwardedOn = new DateTime(2017, 11, 30, 0, 0, 0, DateTimeKind.Utc) },
            new AuthorAwardRecord { Id = 7, AuthorId = 3, AwardId = 4, AwardedOn = new DateTime(2019, 6, 14, 0, 0, 0, DateTimeKind.Utc) },
            new AuthorAwardRecord { Id = 8, AuthorId = 4, AwardId = 1, AwardedOn = new DateTime(2020, 4, 25, 0, 0, 0, DateTimeKind.Utc) },
            new AuthorAwardRecord { Id = 9, AuthorId = 4, AwardId = 2, AwardedOn = new DateTime(2021, 8, 18, 0, 0, 0, DateTimeKind.Utc) },
            new AuthorAwardRecord { Id = 10, AuthorId = 4, AwardId = 3, AwardedOn = new DateTime(2022, 12, 1, 0, 0, 0, DateTimeKind.Utc) },
            new AuthorAwardRecord { Id = 11, AuthorId = 5, AwardId = 4, AwardedOn = new DateTime(2015, 5, 5, 0, 0, 0, DateTimeKind.Utc) },
            new AuthorAwardRecord { Id = 12, AuthorId = 5, AwardId = 1, AwardedOn = new DateTime(2017, 7, 15, 0, 0, 0, DateTimeKind.Utc) },
            new AuthorAwardRecord { Id = 13, AuthorId = 5, AwardId = 3, AwardedOn = new DateTime(2018, 9, 9, 0, 0, 0, DateTimeKind.Utc) },
            new AuthorAwardRecord { Id = 14, AuthorId = 5, AwardId = 2, AwardedOn = new DateTime(2020, 11, 2, 0, 0, 0, DateTimeKind.Utc) },
            new AuthorAwardRecord { Id = 15, AuthorId = 1, AwardId = 4, AwardedOn = new DateTime(2023, 6, 11, 0, 0, 0, DateTimeKind.Utc) }
                );

            modelBuilder.Entity<Book>()
                .HasOne(book=>book.Author)
                .WithMany()
                .HasForeignKey(book=>book.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);  // kaskadno brisanje svih knjiga obrisanog autora

            modelBuilder.Entity<Book>()
                .HasOne(book=>book.Publisher)
                .WithMany()
                .HasForeignKey(book=>book.PublisherId)
                .OnDelete(DeleteBehavior.Cascade);  // kaskadno brisanje svih knjiga obrisanog izdavača
        }
    }
}
