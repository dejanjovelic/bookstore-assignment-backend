using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
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
        private PublishersRepository _publishersRepository;

        public PublishersController(PublishersRepository publishersRepository)
        {
            this._publishersRepository = publishersRepository;
        }


        // GET: api/publishers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _publishersRepository.GetAll());
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while fetching Publishers.");
            }
        }

        // GET api/publishers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                Publisher publisher = await _publishersRepository.GetById(id);
                if (publisher == null)
                {
                    return NotFound();
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
        public async Task<IActionResult> Post(Publisher publisher)
        {
            try
            {
                Publisher newPublisher = await _publishersRepository.Create(publisher);
                return Ok(newPublisher);
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while creating Publisher.");
            }
        }

        // PUT api/publishers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Publisher publisher)
        {
            try
            {
                Publisher existingPublisher = await _publishersRepository.GetById(id);
                if (existingPublisher == null)
                {
                    return NotFound($"Publisher with ID {publisher.Id} not found.");
                }
                existingPublisher.Name = publisher.Name;
                existingPublisher.Adress = publisher.Adress;
                existingPublisher.Website = publisher.Website;
                publisher.Id = id;

                Publisher updatedPublisher = await _publishersRepository.Update(existingPublisher);
                return Ok(updatedPublisher);
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while updating Publisher.");
            }
        }

        // DELETE api/publishers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool result = await _publishersRepository.Delete(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while deleting Publisher.");
            }
        }
    }
}
