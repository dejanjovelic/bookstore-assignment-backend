using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BookstoreApplication.Services.Exceptions;
using BookstoreApplication.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using BookstoreApplication.Services.DTO;

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
            return Ok(await _bookService.GetAllAsync());
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            return Ok(await _bookService.GetByIdAsync(id));
        }

        // POST api/books
        [Authorize(Policy = "CreateBook")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Book book)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Invalid data.");
            }

            return Ok(await _bookService.CreateAsync(book));
        }

        // PUT api/books/5
        [Authorize("EditBook")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Invalid data.");
            }

            return Ok(await _bookService.UpdateAsync(id, book));
        }

        // DELETE api/books/5
        [Authorize("DeleteBook")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _bookService.DeleteAsync(id);
            return NoContent();
        }

        // GET/api/books/sortTypes
        [HttpGet("sortTypes")]
        public IActionResult GetAllBookSortTypes()
        {
            return Ok(_bookService.GetAllSortTypes());
        }

        // GET/api/books/sort
        [HttpGet("sort")]
        public async Task<IActionResult> GetSortedBooksAsync([FromQuery] int sortType = (int)BookSortType.BOOK_TITLE_ASC)
        {
            return Ok(await _bookService.GetSortedBooksAsync(sortType));
        }

        // POST/api/books/filterAndSort?sortType=1
        [HttpPost("filterAndSort")]
        public async Task<IActionResult> GetFilteredAndSortedBooksAsync([FromBody] BookFilterDto filterDto, [FromQuery] int sortType = (int)BookSortType.BOOK_TITLE_ASC)
        {
            return Ok(await _bookService.GetFilteredAnsSortedBooksAsync(filterDto, sortType));
        }
    }
}
