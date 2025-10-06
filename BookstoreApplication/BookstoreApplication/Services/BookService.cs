using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Services
{
    public class BookService
    {
        public BooksRepository _bookRepository;
        private PublishersRepository _publisherService;
        private AuthorService _authorService;

        public BookService(BookstoreDbContext context)
        {
            this._bookRepository = new BooksRepository(context);
            this._authorService = new AuthorService(context);
            this._publisherService = new PublishersRepository(context);
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<Book> CreateAsync(Author author, Publisher publisher, Book book)
        {

            book.Author = author;
            book.Publisher = publisher;
            return await _bookRepository.CreateAsync(book);
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _authorService.GetByIdAsync(id);
        }

        public async Task<Publisher> GetPublisherByIdAsync(int id)
        {
            return await _publisherService.GetByIdAsync(id);
        }

        public async Task<Book> UpdateAsync(Book existingBook, Author bookAuthor, Publisher bookPublisher, Book book)
        {
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

        public async Task DeleteAsync(Book book)
        {
            await _bookRepository.DeleteAsync(book);
        }
    }
}
