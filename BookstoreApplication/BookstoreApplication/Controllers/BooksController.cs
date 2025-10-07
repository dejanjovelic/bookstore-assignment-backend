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
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            this._bookService = bookService;
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
                return Ok(await _bookService.CreateAsync(book));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
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
                if (book.Id != id)
                {
                    return BadRequest($"Book ID mismatch: route ID {id} vs body ID {book.Id}");
                }

                return Ok(await _bookService.UpdateAsync(book));

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

                await _bookService.DeleteAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem("An error occured while deleting Book.");
            }
        }
    }
}
