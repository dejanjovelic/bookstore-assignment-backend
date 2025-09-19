using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {

        private AuthorsRepository _authorsRepository;

        public AuthorsController(BookstoreDbContext context)
        {
            this._authorsRepository = new AuthorsRepository(context);
        }


        // GET: api/authors
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _authorsRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while fetchting Authors.");
            }
        }

        // GET api/authors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            try
            {
                var author = await _authorsRepository.GetByIdAsync(id);
                if (author == null)
                {
                    return NotFound();
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
                Author addedAuthor = await _authorsRepository.CreateAsync(author);
                return Ok(addedAuthor);
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
                Author existingAuthor = await _authorsRepository.GetByIdAsync(author.Id);
                if (existingAuthor == null)
                {
                    return NotFound($"Author with ID {author.Id} not found.");
                }
                author.DateOfBirth = existingAuthor.DateOfBirth;
                author.FullName = existingAuthor.FullName;
                author.Biography = existingAuthor.Biography;
                author.Id = id;

                Author updatedAuthor = await _authorsRepository.UpdateAsync(existingAuthor);
                return Ok(updatedAuthor);
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
                bool result = await _authorsRepository.DeleteAsync(id);
                if (!result)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception)
            {
                return Problem($"An error occured while deleting Author.");
            }
        }
    }
}
