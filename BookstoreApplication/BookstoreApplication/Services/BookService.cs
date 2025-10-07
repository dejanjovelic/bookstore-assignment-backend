using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Services
{
    public class BookService : IBookService
    {
        private IBooksRepository _bookRepository;
        private IPublisherReadService _publisherReadService;
        private IAuthorReadService _authorReadService;

        public BookService(IBooksRepository booksRepository, IAuthorReadService authorReadService, IPublisherReadService publisherReadService)
        {
            this._bookRepository = booksRepository;
            this._authorReadService = authorReadService;
            this._publisherReadService = publisherReadService;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<Book> CreateAsync(Book book)
        {
            Author bookAuthor = await GetAuthorByIdAsync(book.AuthorId);
            if (bookAuthor == null)
            {
                throw new ArgumentException($"Author with ID {book.AuthorId} does not exist.");
            }

            Publisher bookPublisher = await GetPublisherByIdAsync(book.PublisherId);
            if (bookPublisher == null)
            {
                throw new ArgumentException($"Publisher with ID {book.PublisherId} not exist.");
            }

            book.Author = bookAuthor;
            book.Publisher = bookPublisher;
            return await _bookRepository.CreateAsync(book);
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _authorReadService.GetByIdAsync(id);
        }

        public async Task<Publisher> GetPublisherByIdAsync(int id)
        {
            return await _publisherReadService.GetByIdAsync(id);
        }

        public async Task<Book> UpdateAsync( Book book)
        {
            Book existingBook = await GetByIdAsync(book.Id);
            if (existingBook == null)
            {
                throw new ArgumentException($"The Book with ID {book.Id} not exist.");
            }

            Author bookAuthor = await GetAuthorByIdAsync(book.AuthorId);
            if (bookAuthor == null)
            {
                throw new ArgumentException($"Author with ID {book.AuthorId} does not exist.");
            }

            Publisher bookPublisher = await GetPublisherByIdAsync(book.PublisherId);
            if (bookPublisher == null)
            {
                throw new ArgumentException($"Publisher with ID {book.PublisherId} not exist.");
            }

            existingBook.Title = book.Title;
            existingBook.PageCount = book.PageCount;
            existingBook.PublishedDate = book.PublishedDate;
            existingBook.AuthorId = book.AuthorId;
            existingBook.PublisherId = book.PublisherId;
            existingBook.ISBN = book.ISBN;
            existingBook.Author = bookAuthor;
            existingBook.Publisher = bookPublisher;

            return await _bookRepository.UpdateAsync(existingBook);
        }

        public async Task DeleteAsync(int id)
        {
            Book book = await GetByIdAsync(id);
            if (book == null)
            {
                throw new ArgumentException($"The Book with ID {book.Id} not exist.");
            }
            await _bookRepository.DeleteAsync(book);
        }
    }
}
