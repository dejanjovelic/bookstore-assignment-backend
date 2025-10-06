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
    public class PublishersController : ControllerBase
    {
        private readonly PublisherService _publisherService;

        public PublishersController(BookstoreDbContext context)
        {
            this._publisherService = new PublisherService(context);
        }


        // GET: api/publishers
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _publisherService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while fetching Publishers.");
            }
        }

        // GET api/publishers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                Publisher publisher = await _publisherService.GetByIdAsync(id);
                if (publisher == null)
                {
                    return NotFound($"Publisher with ID {id} not found.");
                }
                return Ok(publisher);
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while fetching Publisher.");
            }
        }

        // POST api/publishers
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Publisher publisher)
        {
            try
            {
                return Ok(await _publisherService.CreateAsync(publisher));
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while creating Publisher.");
            }
        }

        // PUT api/publishers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Publisher publisher)
        {
            try
            {
                Publisher existingPublisher = await _publisherService.GetByIdAsync(id);
                if (existingPublisher == null)
                {
                    return NotFound($"Publisher with ID {publisher.Id} not found.");
                }

                return Ok(await _publisherService.UpdateAsync(existingPublisher, publisher));
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while updating Publisher.");
            }
        }

        // DELETE api/publishers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                Publisher publisher = await _publisherService.GetByIdAsync(id);
                if (publisher == null)
                {
                    return NotFound($"Publisher with ID {publisher.Id} not found.");
                }

                await _publisherService.DeleteAsync(publisher);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while deleting Publisher.");
            }
        }
    }
}
