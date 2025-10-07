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

        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            this._authorService = authorService;
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
        public async Task<IActionResult> GetByIdAsync(int id)
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
                    return BadRequest($"Author ID mismatch: route ID {id} vs body ID {author.Id}");
                }

                
                return Ok(await _authorService.UpdateAsync(author));

            }
            catch (ArgumentException ex)
            { 
                return NotFound(ex.Message); 
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
                await _authorService.DeleteAsync(id);
                
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return Problem($"An error occured while deleting Author.");
            }
        }
    }
}
