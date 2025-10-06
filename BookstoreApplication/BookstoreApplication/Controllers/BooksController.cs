using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;
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
        private readonly BookService _bookService;

        public BooksController(BookstoreDbContext context)
        {
            this._bookService = new BookService(context);
        }


        // GET: api/books
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _bookService.GetAllAsync());
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
                Book book = await _bookService.GetByIdAsync(id);
                if (book == null)
                {
                    return NotFound($"The Book with ID {id} not exist.");
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
                Author bookAuthor = await _bookService.GetAuthorByIdAsync(book.AuthorId);
                if (bookAuthor == null)
                {
                    return BadRequest($"Author with ID {book.AuthorId} does not exist.");
                }

                Publisher bookPublisher = await _bookService.GetPublisherByIdAsync(book.PublisherId);
                if (bookPublisher == null)
                {
                    return BadRequest($"Publisher with ID {book.PublisherId} not exist.");
                }

                await _bookService.CreateAsync(bookAuthor,bookPublisher,book);
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
                Book existingBook = await _bookService.GetByIdAsync(id);
                if (existingBook == null)
                {
                    return NotFound($"The Book with ID {book.Id} not exist.");
                }

                Author bookAuthor = await _bookService.GetAuthorByIdAsync(book.AuthorId);
                if (bookAuthor == null)
                {
                    return NotFound($"Author with ID {book.AuthorId} does not exist.");
                }

                Publisher bookPublisher = await _bookService.GetPublisherByIdAsync(book.PublisherId);
                if (bookPublisher == null)
                {
                    return NotFound($"Publisher with ID {book.PublisherId} not exist.");
                }

                return Ok(await _bookService.UpdateAsync(existingBook, bookAuthor, bookPublisher, book));
                
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
                Book book = await _bookService.GetByIdAsync(id);
                if (book == null)
                {
                    return NotFound($"The Book with ID {book.Id} not exist.");
                }

                await _bookService.DeleteAsync(book);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem("An error occured while deleting Book.");
            }
        }
    }
}
