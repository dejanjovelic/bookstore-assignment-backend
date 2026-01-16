using AutoMapper;
using BookstoreApplication.Models;
using BookstoreApplication.Models.IRepositoies;
using BookstoreApplication.Services;
using BookstoreApplication.Services.DTO;
using BookstoreApplication.Services.Exceptions;
using BookstoreApplication.Services.IServices;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Shouldly;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace BookstoreAppTests
{
    public class BookServiceTest
    {

        [Fact]
        public async Task GetAllBooksAsync_ReturnsBooks()
        {
            //Arange
            Author author = new Author
            {
                Id = 1,
                FullName = "John Smith",
                Biography = "Pisac romana i eseja.",
                DateOfBirth = new DateTime(1975, 5, 12, 0, 0, 0, DateTimeKind.Utc)
            };

            Publisher publisher = new Publisher
            {
                Id = 1,
                Name = "Sunrise Books",
                Adress = "Main Street 12",
                Website = "https://sunrisebooks.com"
            };

            Book book = new Book
            {
                Id = 1,
                Title = "Rivers of Time",
                PageCount = 320,
                PublishedDate = new DateTime(2010, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                ISBN = "978000000001",
                AuthorId = author.Id,
                PublisherId = publisher.Id
            };
            Book book1 = new Book
            {
                Id = 3,
                Title = "Magic Adventures",
                PageCount = 150,
                PublishedDate = new DateTime(2015, 3, 15, 0, 0, 0, DateTimeKind.Utc),
                ISBN = "978000000003",
                AuthorId = author.Id,
                PublisherId = publisher.Id
            };
            Book book2 = new Book
            {
                Id = 4,
                Title = "Forest Tales",
                PageCount = 180,
                PublishedDate = new DateTime(2018, 8, 20, 0, 0, 0, DateTimeKind.Utc),
                ISBN = "978000000004",
                AuthorId = author.Id,
                PublisherId = publisher.Id
            };

            List<Book> books = new List<Book> { book, book1, book2 };

            BookDto bookDto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                AuthorFullName = author.FullName,
                PublisherName = publisher.Name,
                YearsSincePublication = DateTime.Today.Year - book.PublishedDate.Year
            };

            BookDto bookDto1 = new BookDto
            {
                Id = book1.Id,
                Title = book1.Title,
                ISBN = book1.ISBN,
                AuthorFullName = author.FullName,
                PublisherName = publisher.Name,
                YearsSincePublication = DateTime.Today.Year - book1.PublishedDate.Year
            };

            BookDto bookDto2 = new BookDto
            {
                Id = book2.Id,
                Title = book2.Title,
                ISBN = book2.ISBN,
                AuthorFullName = author.FullName,
                PublisherName = publisher.Name,
                YearsSincePublication = DateTime.Today.Year - book2.PublishedDate.Year
            };

            List<BookDto> booksDtos = new List<BookDto> { bookDto, bookDto1, bookDto2 };

            var mockBooksRepository = Substitute.For<IBooksRepository>();
            mockBooksRepository.GetAllAsync().Returns(books);

            var mockPublisherReadService = Substitute.For<IPublisherReadService>();

            var mockAuthorReadService = Substitute.For<IAuthorReadService>();

            var mockMapper = Substitute.For<IMapper>();
            mockMapper.Map<List<BookDto>>(books).Returns(booksDtos);

            var mockLogger = Substitute.For<ILogger<BookService>>();

            var service = new BookService(mockBooksRepository, mockAuthorReadService, mockPublisherReadService, mockMapper, mockLogger);

            //Act
            var result = await service.GetAllAsync();

            //Assert
            Assert.NotNull(result);
            result.Count().ShouldBe(3);
        }


        [Fact]
        public async Task GetBookByIdAsync_ReturnsBook_WhenIdIsValid()
        {
            //Arange

            Author author = new Author
            {
                Id = 1,
                FullName = "John Smith",
                Biography = "Pisac romana i eseja.",
                DateOfBirth = new DateTime(1975, 5, 12, 0, 0, 0, DateTimeKind.Utc)
            };

            Publisher publisher = new Publisher
            {
                Id = 1,
                Name = "Sunrise Books",
                Adress = "Main Street 12",
                Website = "https://sunrisebooks.com"
            };

            Book book = new Book
            {
                Id = 1,
                Title = "Rivers of Time",
                PageCount = 320,
                PublishedDate = new DateTime(2010, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                ISBN = "978000000001",
                AuthorId = author.Id,
                PublisherId = publisher.Id
            };

            BookDetailsDto bookDetailsDto = new BookDetailsDto
            {
                Id = book.Id,
                Title = book.Title,
                PageCount = book.PageCount,
                PublishedDate = book.PublishedDate,
                ISBN = book.ISBN,
                AuthorId = author.Id,
                AuthorFullName = author.FullName,
                PublisherId = publisher.Id,
                PublisherName = publisher.Name
            };

            var mockBooksRepository = Substitute.For<IBooksRepository>();
            mockBooksRepository.GetByIdAsync(1).Returns(book);


            var mockPublisherReadService = Substitute.For<IPublisherReadService>();

            var mockAuthorReadService = Substitute.For<IAuthorReadService>();

            var mockMapper = Substitute.For<IMapper>();
            mockMapper.Map<BookDetailsDto>(book).Returns(bookDetailsDto);

            var mockLogger = Substitute.For<ILogger<BookService>>();

            var service = new BookService(mockBooksRepository, mockAuthorReadService, mockPublisherReadService, mockMapper, mockLogger);

            //Act

            var result = await service.GetByIdAsync(book.Id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(bookDetailsDto.Id, result.Id);

        }


        [Fact]
        public async Task GetBookByIdAsync_ThrowsNotFoundException_WhenIdIsNotValid()
        {
            //Arange
            NotFoundException mockException = new NotFoundException("The Book with ID 16 not exist.");
            var mockBooksRepository = Substitute.For<IBooksRepository>();

            var mockPublisherReadService = Substitute.For<IPublisherReadService>();

            var mockAuthorReadService = Substitute.For<IAuthorReadService>();

            var mockMapper = Substitute.For<IMapper>();

            var mockLogger = Substitute.For<ILogger<BookService>>();

            var service = new BookService(mockBooksRepository, mockAuthorReadService, mockPublisherReadService, mockMapper, mockLogger);

            //Act

            var result2 = () => service.GetByIdAsync(16);

            //Assert
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(result2);
            Assert.Equal(mockException.Message, exception.Message);

        }

        [Fact]
        public async Task CreateAsync_CreateNewBook_IfNewBookDataIsValid()
        {
            //Arange
            Author author = new Author
            {
                Id = 1,
                FullName = "John Smith",
                Biography = "Pisac romana i eseja.",
                DateOfBirth = new DateTime(1975, 5, 12, 0, 0, 0, DateTimeKind.Utc)
            };

            Publisher publisher = new Publisher
            {
                Id = 1,
                Name = "Sunrise Books",
                Adress = "Main Street 12",
                Website = "https://sunrisebooks.com"
            };

            Book book = new Book
            {
                Id = 1,
                Title = "Rivers of Time",
                PageCount = 320,
                PublishedDate = new DateTime(2010, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                ISBN = "978000000001",
                AuthorId = author.Id,
                PublisherId = publisher.Id
            };

            var mockBooksRepository = Substitute.For<IBooksRepository>();
            mockBooksRepository.CreateAsync(book).Returns(book);

            var mockPublisherReadService = Substitute.For<IPublisherReadService>();
            mockPublisherReadService.GetByIdAsync(publisher.Id).Returns(publisher);

            var mockAuthorReadService = Substitute.For<IAuthorReadService>();
            mockAuthorReadService.GetByIdAsync(author.Id).Returns(author);


            var mockMapper = Substitute.For<IMapper>();

            var mockLogger = Substitute.For<ILogger<BookService>>();

            var service = new BookService(mockBooksRepository, mockAuthorReadService, mockPublisherReadService, mockMapper, mockLogger);

            //Act
            var result = await service.CreateAsync(book);

            //Assert
            result.ShouldNotBe(null);
            result.Id.ShouldBe(book.Id);
            result.Author.ShouldNotBeNull();
            result.Author.Id.ShouldBe(author.Id);
            result.Publisher.ShouldNotBeNull();
            result.Publisher.Id.ShouldBe(publisher.Id);
            await mockBooksRepository.Received(1).CreateAsync(book);
        }

        [Fact]
        public async Task CreateAsync_ThrowsNotFoundException_IfAuthorIdIsNotValid()
        {
            //Arange

            Publisher publisher = new Publisher
            {
                Id = 1,
                Name = "Sunrise Books",
                Adress = "Main Street 12",
                Website = "https://sunrisebooks.com"
            };

            Book book = new Book
            {
                Id = 1,
                Title = "Rivers of Time",
                PageCount = 320,
                PublishedDate = new DateTime(2010, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                ISBN = "978000000001",
                AuthorId = 12,
                PublisherId = publisher.Id
            };

            var mockBooksRepository = Substitute.For<IBooksRepository>();

            var mockAuthorReadService = Substitute.For<IAuthorReadService>();
            mockAuthorReadService.GetByIdAsync(12).Returns((Author)null);

            var mockLogger = Substitute.For<ILogger<BookService>>();

            var service = new BookService(mockBooksRepository, mockAuthorReadService, null, null, mockLogger);

            // Act & Assert
            NotFoundException resultException = await Assert.ThrowsAsync<NotFoundException>(() => service.CreateAsync(book));
            resultException.Message.ShouldBe($"Author with ID 12 does not exist.");
            await mockBooksRepository.DidNotReceive().CreateAsync(book);
        }

        [Fact]
        public async Task CreateAsync_ThrowsNotFoundException_IfPublisherIdIsNotValid()
        {
            //Arange
            Author author = new Author
            {
                Id = 1,
                FullName = "John Smith",
                Biography = "Pisac romana i eseja.",
                DateOfBirth = new DateTime(1975, 5, 12, 0, 0, 0, DateTimeKind.Utc)
            };

            Book book = new Book
            {
                Id = 1,
                Title = "Rivers of Time",
                PageCount = 320,
                PublishedDate = new DateTime(2010, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                ISBN = "978000000001",
                AuthorId = author.Id,
                PublisherId = 3
            };

            var mockBooksRepository = Substitute.For<IBooksRepository>();

            var mockPublisherReadService = Substitute.For<IPublisherReadService>();
            mockPublisherReadService.GetByIdAsync(3).Returns((Publisher)null);

            var mockAuthorReadService = Substitute.For<IAuthorReadService>();
            mockAuthorReadService.GetByIdAsync(author.Id).Returns(author);

            var mockLogger = Substitute.For<ILogger<BookService>>();

            var service = new BookService(mockBooksRepository, mockAuthorReadService, mockPublisherReadService, null, mockLogger);

            //Act & Assert
            NotFoundException resultException = await Assert.ThrowsAsync<NotFoundException>(() => service.CreateAsync(book));
            resultException.Message.ShouldBe("Publisher with ID 3 not exist.");
            await mockBooksRepository.DidNotReceive().CreateAsync(book);
        }

        [Fact]
        public async Task UpdateAsync_SuccessfulyUpdateBook_IfBookId_And_BookData_AreValid()
        {
            //Arange
            Author author = new Author
            {
                Id = 1,
                FullName = "John Smith",
                Biography = "Pisac romana i eseja.",
                DateOfBirth = new DateTime(1975, 5, 12, 0, 0, 0, DateTimeKind.Utc)
            };

            Publisher publisher = new Publisher
            {
                Id = 1,
                Name = "Sunrise Books",
                Adress = "Main Street 12",
                Website = "https://sunrisebooks.com"
            };

            Book book = new Book
            {
                Id = 1,
                Title = "Rivers of Time",
                PageCount = 320,
                PublishedDate = new DateTime(2010, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                ISBN = "978000000001",
                AuthorId = author.Id,
                PublisherId = publisher.Id
            };

            Book UpdateBook = new Book
            {
                Id = 1,
                Title = "Rivers of Time New part",
                PageCount = 360,
                PublishedDate = new DateTime(2010, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                ISBN = "978000000200",
                AuthorId = author.Id,
                PublisherId = publisher.Id
            };

            var mockBooksRepository = Substitute.For<IBooksRepository>();
            mockBooksRepository.GetByIdAsync(book.Id).Returns(book);
            mockBooksRepository.UpdateAsync(Arg.Any<Book>())
                                .Returns(callInfo => callInfo.Arg<Book>());

            var mockPublisherReadService = Substitute.For<IPublisherReadService>();
            mockPublisherReadService.GetByIdAsync(book.PublisherId).Returns(publisher);

            var mockAuthorReadService = Substitute.For<IAuthorReadService>();
            mockAuthorReadService.GetByIdAsync(book.AuthorId).Returns(author);
            var mockLogger = Substitute.For<ILogger<BookService>>();

            var service = new BookService(mockBooksRepository, mockAuthorReadService, mockPublisherReadService, null, mockLogger);

            //Act
            var result = await service.UpdateAsync(book.Id, UpdateBook);

            //Assert
            result.ShouldNotBe(null);
            result.Title.ShouldBe(UpdateBook.Title);
            result.ISBN.ShouldBe(UpdateBook.ISBN);
            result.PageCount.ShouldBe(UpdateBook.PageCount);
            await mockBooksRepository.Received(1).UpdateAsync(Arg.Any<Book>());

        }

        [Fact]
        public async Task UpdateAsync_ThrowsNotFoundException_IfBookWithProvidedIdNotExist()
        {
            //Arange
            Author author = new Author
            {
                Id = 1,
                FullName = "John Smith",
                Biography = "Pisac romana i eseja.",
                DateOfBirth = new DateTime(1975, 5, 12, 0, 0, 0, DateTimeKind.Utc)
            };

            Publisher publisher = new Publisher
            {
                Id = 1,
                Name = "Sunrise Books",
                Adress = "Main Street 12",
                Website = "https://sunrisebooks.com"
            };



            Book UpdateBook = new Book
            {
                Id = 12,
                Title = "Rivers of Time New part",
                PageCount = 360,
                PublishedDate = new DateTime(2010, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                ISBN = "978000000200",
                AuthorId = author.Id,
                PublisherId = publisher.Id
            };

            var mockBooksRepository = Substitute.For<IBooksRepository>();
            mockBooksRepository.GetByIdAsync(12).Returns((Book)null);


            var mockPublisherReadService = Substitute.For<IPublisherReadService>();


            var mockAuthorReadService = Substitute.For<IAuthorReadService>();
            var mockLogger = Substitute.For<ILogger<BookService>>();

            var service = new BookService(mockBooksRepository, mockAuthorReadService, mockPublisherReadService, null, mockLogger);

            //Act & Assert

            NotFoundException resultException = await Assert.ThrowsAsync<NotFoundException>(() => service.UpdateAsync(12, UpdateBook));
            resultException.Message.ShouldBe("The Book with ID 12 not exist.");
            await mockBooksRepository.Received(1).GetByIdAsync(12);

        }

        [Fact]
        public async Task UpdateAsync_ThrowsBadRequestException_IfBookIdIsNotEqualWithUpdateBookId()
        {
            //Arange
            Author author = new Author
            {
                Id = 1,
                FullName = "John Smith",
                Biography = "Pisac romana i eseja.",
                DateOfBirth = new DateTime(1975, 5, 12, 0, 0, 0, DateTimeKind.Utc)
            };

            Publisher publisher = new Publisher
            {
                Id = 1,
                Name = "Sunrise Books",
                Adress = "Main Street 12",
                Website = "https://sunrisebooks.com"
            };

            Book book = new Book
            {
                Id = 1,
                Title = "Rivers of Time",
                PageCount = 320,
                PublishedDate = new DateTime(2010, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                ISBN = "978000000001",
                AuthorId = author.Id,
                PublisherId = publisher.Id
            };

            Book UpdateBook = new Book
            {
                Id = 12,
                Title = "Rivers of Time New part",
                PageCount = 360,
                PublishedDate = new DateTime(2010, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                ISBN = "978000000200",
                AuthorId = author.Id,
                PublisherId = publisher.Id
            };

            var mockBooksRepository = Substitute.For<IBooksRepository>();
            mockBooksRepository.GetByIdAsync(book.Id).Returns(book);


            var mockPublisherReadService = Substitute.For<IPublisherReadService>();


            var mockAuthorReadService = Substitute.For<IAuthorReadService>();
            var mockLogger = Substitute.For<ILogger<BookService>>();

            var service = new BookService(mockBooksRepository, mockAuthorReadService, mockPublisherReadService, null, mockLogger);

            //Act & Assert

            BadRequestException resultException = await Assert.ThrowsAsync<BadRequestException>(() => service.UpdateAsync(book.Id, UpdateBook));
            resultException.Message.ShouldBe($"Book ID mismatch: route ID 1 vs body ID 12");

        }

        [Fact]
        public async Task UpdateAsync_ThrowsNotFoundException_IfAuthorIdIsNotValid()
        {
            //Arange

            Publisher publisher = new Publisher
            {
                Id = 1,
                Name = "Sunrise Books",
                Adress = "Main Street 12",
                Website = "https://sunrisebooks.com"
            };

            Book book = new Book
            {
                Id = 1,
                Title = "Rivers of Time",
                PageCount = 320,
                PublishedDate = new DateTime(2010, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                ISBN = "978000000001",
                AuthorId = 12,
                PublisherId = publisher.Id
            };

            Book UpdateBook = new Book
            {
                Id = 1,
                Title = "Rivers of Time New part",
                PageCount = 360,
                PublishedDate = new DateTime(2010, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                ISBN = "978000000200",
                AuthorId = 12,
                PublisherId = publisher.Id
            };

            var mockBooksRepository = Substitute.For<IBooksRepository>();
            mockBooksRepository.GetByIdAsync(book.Id).Returns(book);

            var mockPublisherReadService = Substitute.For<IPublisherReadService>();
            mockPublisherReadService.GetByIdAsync(book.PublisherId).Returns(publisher);

            var mockAuthorReadService = Substitute.For<IAuthorReadService>();
            mockAuthorReadService.GetByIdAsync(12).Returns((Author)null);

            var mockLogger = Substitute.For<ILogger<BookService>>();

            var service = new BookService(mockBooksRepository, mockAuthorReadService, mockPublisherReadService, null, mockLogger);


            //Act & Assert
            NotFoundException resultException = await Assert.ThrowsAsync<NotFoundException>(() => service.UpdateAsync(book.Id, UpdateBook));
            await mockBooksRepository.Received(1).GetByIdAsync(book.Id);
            await mockAuthorReadService.Received(1).GetByIdAsync(book.AuthorId);
            resultException.Message.ShouldBe($"Author with ID {book.AuthorId} does not exist.");
        }

        [Fact]
        public async Task UpdateAsync_ThrowsNotFoundException_IfPublisherIdIsNotValid()
        {
            //Arange
            Author author = new Author
            {
                Id = 1,
                FullName = "John Smith",
                Biography = "Pisac romana i eseja.",
                DateOfBirth = new DateTime(1975, 5, 12, 0, 0, 0, DateTimeKind.Utc)
            };

            Book book = new Book
            {
                Id = 1,
                Title = "Rivers of Time",
                PageCount = 320,
                PublishedDate = new DateTime(2010, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                ISBN = "978000000001",
                AuthorId = author.Id,
                PublisherId = 10
            };

            Book UpdateBook = new Book
            {
                Id = 1,
                Title = "Rivers of Time New part",
                PageCount = 360,
                PublishedDate = new DateTime(2010, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                ISBN = "978000000200",
                AuthorId = author.Id,
                PublisherId = 10
            };

            var mockBooksRepository = Substitute.For<IBooksRepository>();
            mockBooksRepository.GetByIdAsync(book.Id).Returns(book);

            var mockAuthorReadService = Substitute.For<IAuthorReadService>();
            mockAuthorReadService.GetByIdAsync(book.AuthorId).Returns(author);

            var mockPublisherReadService = Substitute.For<IPublisherReadService>();
            mockPublisherReadService.GetByIdAsync(10).Returns((Publisher)null);

           
            var mockLogger = Substitute.For<ILogger<BookService>>();

            var service = new BookService(mockBooksRepository, mockAuthorReadService, mockPublisherReadService, null, mockLogger);


            //Act & Assert
            NotFoundException resultException = await Assert.ThrowsAsync<NotFoundException>(() => service.UpdateAsync(book.Id, UpdateBook));
            resultException.Message.ShouldBe("Publisher with ID 10 not exist.");
            await mockBooksRepository.Received(1).GetByIdAsync(book.Id);

        }
    }
}