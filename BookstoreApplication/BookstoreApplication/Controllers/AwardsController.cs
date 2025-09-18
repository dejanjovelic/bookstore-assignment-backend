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
        private AwardsRepository _awardsRepository;

        public AwardsController(AwardsRepository awardsRepository)
        {
            _awardsRepository = awardsRepository;
        }



        //api/awards
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _awardsRepository.GetAll());
            }
            catch (Exception)
            {
                return Problem("An error occured while fetching awards.");
            }
        }

        //api/awards/2
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                Award award = await _awardsRepository.GetById(id);
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
        public async Task<IActionResult> Post([FromBody] Award award)
        {
            try
            {
                Award newAward = await _awardsRepository.Create(award);
                return Ok(newAward);
            }
            catch (Exception)
            {
                return Problem("An error occured while creating award.");
            }
        }

        //api/awards/2
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] Award award)
        {
            try
            {
                Award existingAward = await _awardsRepository.GetById(id);
                if (existingAward == null)
                {
                    return NotFound();
                }

                existingAward.Id = id;
                existingAward.Name = award.Name;
                existingAward.Description = award.Description;
                existingAward.AwardStartYear = award.AwardStartYear;
                award = await _awardsRepository.Update(existingAward);
                return Ok(award);
            }
            catch (Exception)
            {
                return Problem("An error occured while updating award.");
            }
        }

        //api/awards/2
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool result = await _awardsRepository.Delete(id);
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
