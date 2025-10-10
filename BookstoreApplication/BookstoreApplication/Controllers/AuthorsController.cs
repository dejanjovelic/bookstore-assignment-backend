using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {

        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            this._authorService = authorService;
        }


        // GET: api/authors
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _authorService.GetAllAsync());
        }

        // GET api/authors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _authorService.GetByIdAsync(id));
        }

        // POST api/authors
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Author author)
        {
            return Ok(await _authorService.CreateAsync(author));
        }

        // PUT api/authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Author author)
        {
            return Ok(await _authorService.UpdateAsync(id, author));

        }

        // DELETE api/authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _authorService.DeleteAsync(id);
            return NoContent();
        }

        // GET api/authors/paging?page=1&&rowsPerPage
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllAuthorsPaginatedAsync([FromQuery] int page, [FromQuery] int pageSize)
        {
            return Ok(await _authorService.GetAllAuthorsPaginatedAsync(page, pageSize));
        }
    }
}
