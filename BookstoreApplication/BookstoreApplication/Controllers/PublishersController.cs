using BookstoreApplication.Data;
using BookstoreApplication.DTO;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services.IServices;
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
            return Ok(await _publisherService.GetAllAsync());
        }

        // GET api/publishers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _publisherService.GetByIdAsync(id));
        }

        // POST api/publishers
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Invalid data.");
            }

            return Ok(await _publisherService.CreateAsync(publisher));
        }

        // PUT api/publishers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Invalid data.");
            }

            return Ok(await _publisherService.UpdateAsync(id, publisher));
        }

        // DELETE api/publishers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _publisherService.DeleteAsync(id);
            return NoContent();
        }


        [HttpGet("sortTypes")]
        public ActionResult<List<PublisherSortTypeOptionDto>> GetAllSortTypes()
        {
            return _publisherService.GetAllSortTypes();
        }

        [HttpGet("sort")]
        public async Task<IEnumerable<Publisher>> GetSortedPublishers([FromQuery] int sortType = (int)PublisherSortType.NAME_ASCENDING)
        {
            return await _publisherService.GetSortedPublishers(sortType);
        }
    }
}
