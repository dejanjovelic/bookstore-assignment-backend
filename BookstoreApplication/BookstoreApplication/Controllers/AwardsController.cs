using BookstoreApplication.Models;
using BookstoreApplication.Services.Exceptions;
using BookstoreApplication.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookstoreApplication.Services.IServices;

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
            return Ok(await _awardService.GetAllAsync());
        }

        //api/awards/2
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _awardService.GetByIdAsync(id));
        }

        //api/awards
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Award award)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Invalid data.");
            }
            return Ok(await _awardService.CreateAsync(award));
        }

        //api/awards/2
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Award award)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Invalid data.");
            }
            return Ok(await _awardService.UpdateAsync(id, award));
        }

        //api/awards/2
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _awardService.DeleteAsync(id);
            return NoContent();
        }
    }
}
