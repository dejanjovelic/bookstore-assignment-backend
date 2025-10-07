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
        private readonly IPublisherService _publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            this._publisherService = publisherService;
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
                if (publisher.Id != id)
                {
                    return BadRequest($"Publisher ID mismatch: route ID:{id} body ID:{publisher.Id}.");
                }
               

                return Ok(await _publisherService.UpdateAsync( publisher));
            }
            catch(ArgumentException ex)
            { 
                return NotFound(ex.Message); 
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
                await _publisherService.DeleteAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while deleting Publisher.");
            }
        }
    }
}
