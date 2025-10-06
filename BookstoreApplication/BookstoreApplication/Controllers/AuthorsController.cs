using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {

        private readonly AuthorService _authorService;

        public AuthorsController(BookstoreDbContext context)
        {
            this._authorService = new AuthorService(context);
        }


        // GET: api/authors
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _authorService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while fetching Authors.");
            }
        }

        // GET api/authors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            try
            {
                var author = await _authorService.GetByIdAsync(id);
                if (author == null)
                {
                    return NotFound($"Author with ID {id} not found.");
                }
                return Ok(author);
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while fetchting Author.");
            }
        }

        // POST api/authors
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Author author)
        {
            try
            {
                return Ok(await _authorService.CreateAsync(author));
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while creating Author.");
            }
        }

        // PUT api/authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Author author)
        {
            try
            {
                if (author.Id != id) 
                {
                    return Problem($"Author ID mismatch: route ID {id} vs body ID {author.Id}");
                }

                Author existingAuthor = await _authorService.GetByIdAsync(author.Id);
                if (existingAuthor == null)
                {
                    return NotFound($"Author with ID {author.Id} not found.");
                }

                return Ok(await _authorService.UpdateAsync(author, existingAuthor));

            }
            catch (Exception)
            {
                return Problem("An error occured while updating Author.");
            }
        }

        // DELETE api/authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                Author author = await _authorService.GetByIdAsync(id);
                if (author == null)
                {
                    return NotFound($"Author with ID {id} not found.");
                }

                await _authorService.DeleteAsync(author);
                
                return NoContent();
            }
            catch (Exception)
            {
                return Problem($"An error occured while deleting Author.");
            }
        }
    }
}
