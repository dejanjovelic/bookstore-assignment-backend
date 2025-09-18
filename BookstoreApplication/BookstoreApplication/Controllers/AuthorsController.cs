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

        public AuthorsController(AuthorsRepository authorsRepository)
        {
            this._authorsRepository = authorsRepository;
        }


        // GET: api/authors
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _authorsRepository.GetAll());
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while fetchting Authors.");
            }
        }

        // GET api/authors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                var author = await _authorsRepository.GetById(id);
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
        public async Task<IActionResult> Post(Author author)
        {
            try
            {
                Author addedAuthor = await _authorsRepository.Create(author);
                return Ok(addedAuthor);
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while creating Author.");
            }
        }

        // PUT api/authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Author author)
        {
            try
            {
                Author existingAuthor = await _authorsRepository.GetById(author.Id);
                if (existingAuthor == null)
                {
                    return NotFound($"Author with ID {author.Id} not found.");
                }
                author.DateOfBirth = existingAuthor.DateOfBirth;
                author.FullName = existingAuthor.FullName;
                author.Biography = existingAuthor.Biography;
                author.Id = id;

                Author updatedAuthor = await _authorsRepository.Update(existingAuthor);
                return Ok(updatedAuthor);
            }
            catch (Exception)
            {
                return Problem("An error occured while updating Author.");
            }
        }

        // DELETE api/authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool result = await _authorsRepository.Delete(id);
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
