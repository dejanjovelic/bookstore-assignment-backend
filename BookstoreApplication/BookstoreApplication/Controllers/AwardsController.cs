using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardsController : ControllerBase
    {
        private readonly AwardsRepository _awardsRepository;

        public AwardsController(BookstoreDbContext context)
        {
            _awardsRepository = new AwardsRepository(context);
        }



        //api/awards
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _awardsRepository.GetAllAsync());
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
                Award award = await _awardsRepository.GetByIdAsync(id);
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
                Award newAward = await _awardsRepository.CreateAsync(award);
                return Ok(newAward);
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
                Award existingAward = await _awardsRepository.GetByIdAsync(id);
                if (existingAward == null)
                {
                    return NotFound();
                }

                existingAward.Id = id;
                existingAward.Name = award.Name;
                existingAward.Description = award.Description;
                existingAward.AwardStartYear = award.AwardStartYear;
                award = await _awardsRepository.UpdateAsync(existingAward);
                return Ok(award);
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
                bool result = await _awardsRepository.DeleteAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception)
            {
                return Problem("An error occured while deleting award.");
            }
        }

    }
}
