using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private BooksRepository _booksRepository;
        private AuthorsRepository _authorsRepository;
        private PublishersRepository _publishersRepository;

        public BooksController(BookstoreDbContext context)
        {
            this._booksRepository = new BooksRepository(context);
            this._authorsRepository = new AuthorsRepository(context);
            this._publishersRepository = new PublishersRepository(context);
        }


        // GET: api/books
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _booksRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                return Problem("An error occured while fetching Books.");
            }
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            try
            {
                Book book = await _booksRepository.GetByIdAsync(id);
                if (book == null)
                {
                    return NotFound();
                }
                return Ok(book);
            }
            catch (Exception ex)
            {
                return Problem("An error occured while fetching Book.");
            }
        }

        // POST api/books
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Book book)
        {
            try
            {
                Author bookAuthor = await _authorsRepository.GetByIdAsync(book.AuthorId);
                if (bookAuthor == null)
                {
                    return BadRequest($"Author with ID {book.AuthorId} does not exist.");
                }

                Publisher bookPublisher = await _publishersRepository.GetByIdAsync(book.PublisherId);
                if (bookPublisher == null)
                {
                    return BadRequest($"Publisher with ID {book.PublisherId} not exist.");
                }

                book.Author = bookAuthor;
                book.Publisher = bookPublisher;
                await _booksRepository.CreateAsync(book);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return Problem("An error occured while creating Book.");
            }
        }

        // PUT api/books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Book book)
        {
            try
            {
                Book existingBook = await _booksRepository.GetByIdAsync(id);
                if (existingBook == null)
                {
                    return NotFound($"The Book with ID {book.Id} not exist.");
                }

                Author bookAuthor = await _authorsRepository.GetByIdAsync(book.AuthorId);
                if (bookAuthor == null)
                {
                    return NotFound($"Author with ID {book.AuthorId} does not exist.");
                }

                Publisher bookPublisher = await _publishersRepository.GetByIdAsync(book.PublisherId);
                if (bookPublisher == null)
                {
                    return NotFound($"Publisher with ID {book.PublisherId} not exist.");
                }

                existingBook.Title = book.Title;
                existingBook.PageCount = book.PageCount;
                existingBook.PublishedDate = book.PublishedDate;
                existingBook.AuthorId = book.AuthorId;
                existingBook.PublisherId = book.PublisherId;
                existingBook.ISBN = book.ISBN;
                existingBook.Author = bookAuthor;
                existingBook.Publisher = bookPublisher;

                Book updatedBook = await _booksRepository.UpdateAsync(existingBook);
                return Ok(updatedBook);
            }
            catch (Exception ex)
            {
                return Problem("An error occured while updating Book.");
            }
        }

        // DELETE api/books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                bool result = await _booksRepository.DeleteAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem("An error occured while deleting Book.");
            }
        }
    }
}
