using AutoMapper;
using BookstoreApplication.Exceptions;
using BookstoreApplication.DTO;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Services
{
    public class BookService : IBookService
    {
        private readonly IBooksRepository _bookRepository;
        private readonly IPublisherReadService _publisherReadService;
        private readonly IAuthorReadService _authorReadService;
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;

        public BookService(IBooksRepository booksRepository, IAuthorReadService authorReadService, IPublisherReadService publisherReadService, IMapper mapper, ILogger<BookService> logger)
        {
            this._bookRepository = booksRepository;
            this._authorReadService = authorReadService;
            this._publisherReadService = publisherReadService;
            this._mapper = mapper;
            _logger = logger;
        }

        public async Task<List<BookDto>> GetAllAsync()
        {
            _logger.LogInformation($"Geting all books.");
            List<Book> books = await _bookRepository.GetAllAsync();
            return books
                .Select(_mapper.Map<BookDto>)
                .ToList();
        }

        public async Task<BookDetailsDto> GetByIdAsync(int id)
        {
            _logger.LogInformation($"Get book with id {id}.");
            Book book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                _logger.LogError($"The Book with ID: {id} not exist.");
                throw new NotFoundException($"The Book with ID {id} not exist.");
            }
            _logger.LogInformation($"The Book with Id: {id} exists.");
            return _mapper.Map<BookDetailsDto>(book);
        }

        public async Task<Book> CreateAsync(Book book)
        {
            _logger.LogInformation($"Creating book: {book.Id}");
            Author bookAuthor = await GetAuthorByIdAsync(book.AuthorId);
            if (bookAuthor == null)
            {
                _logger.LogError($"Author with ID {book.AuthorId} does not exist.");
                throw new NotFoundException($"Author with ID {book.AuthorId} does not exist.");
            }

            Publisher bookPublisher = await GetPublisherByIdAsync(book.PublisherId);
            if (bookPublisher == null)
            {
                _logger.LogError($"Publisher with ID {book.PublisherId} not exist.");
                throw new NotFoundException($"Publisher with ID {book.PublisherId} not exist.");
            }

            book.Author = bookAuthor;
            book.Publisher = bookPublisher;
            _logger.LogInformation($"Created Book Id:{book.Id} Title: {book.Title}");
            return await _bookRepository.CreateAsync(book);
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            _logger.LogInformation($"Get Author with id {id}.");
            return await _authorReadService.GetByIdAsync(id);
        }

        public async Task<Publisher> GetPublisherByIdAsync(int id)
        {
            _logger.LogInformation($"Get Publisher with id {id}.");
            return await _publisherReadService.GetByIdAsync(id);
        }

        public async Task<Book> UpdateAsync(int id, Book book)
        {
            _logger.LogInformation($"Updating book with ID: {id}");
            if (book.Id != id)
            {
                throw new BadRequestException($"Book ID mismatch: route ID {id} vs body ID {book.Id}");
            }

            Book existingBook = await _bookRepository.GetByIdAsync(book.Id);
            if (existingBook == null)
            {
                throw new NotFoundException($"The Book with ID {book.Id} not exist.");
            }

            Author bookAuthor = await GetAuthorByIdAsync(book.AuthorId);
            if (bookAuthor == null)
            {
                throw new NotFoundException($"Author with ID {book.AuthorId} does not exist.");
            }

            Publisher bookPublisher = await GetPublisherByIdAsync(book.PublisherId);
            if (bookPublisher == null)
            {
                throw new NotFoundException($"Publisher with ID {book.PublisherId} not exist.");
            }

            existingBook.Title = book.Title;
            existingBook.PageCount = book.PageCount;
            existingBook.PublishedDate = book.PublishedDate;
            existingBook.AuthorId = book.AuthorId;
            existingBook.PublisherId = book.PublisherId;
            existingBook.ISBN = book.ISBN;
            existingBook.Author = bookAuthor;
            existingBook.Publisher = bookPublisher;

            _logger.LogInformation($"Book with ID: {id} updated. ");
            return await _bookRepository.UpdateAsync(existingBook);
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation($"Deleting book with ID: {id}");
            Book book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                throw new NotFoundException($"The Book with ID {id} not exist.");
            }
            _logger.LogInformation($"{book.Title} with ID: {id} deleted.");
            await _bookRepository.DeleteAsync(book);
        }
    }
}
