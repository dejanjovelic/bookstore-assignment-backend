using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardsController : ControllerBase
    {
        private readonly IAwardService _awardService;

        public AwardsController(IAwardService awardService)
        {
            _awardService = awardService;
        }



        //api/awards
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _awardService.GetAllAsync());
            }
            catch (Exception)
            {
                return Problem("An error occured while fetching awards.");
            }
        }

        //api/awards/2
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                Award award = await _awardService.GetByIdAsync(id);
                if (award == null)
                {
                    return NotFound();
                }
                return Ok(award);
            }
            catch (Exception)
            {
                return Problem("An error occured while fetching award.");
            }
        }

        //api/awards
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Award award)
        {
            try
            {
                return Ok(await _awardService.CreateAsync(award));
            }
            catch (Exception)
            {
                return Problem("An error occured while creating award.");
            }
        }

        //api/awards/2
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Award award)
        {
            try
            {
                if (award.Id != id)
                {
                    return BadRequest($"Award ID mismatch: route ID {id} vs body ID {award.Id}");
                }

                return Ok(await _awardService.UpdateAsync(award));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return Problem("An error occured while updating award.");
            }
        }

        //api/awards/2
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _awardService.DeleteAsync(id);
                return NoContent();
            }
            catch(ArgumentException ex)
            { 
                return NotFound(ex.Message); 
            }
            catch (Exception)
            {
                return Problem("An error occured while deleting award.");
            }
        }

    }
}
