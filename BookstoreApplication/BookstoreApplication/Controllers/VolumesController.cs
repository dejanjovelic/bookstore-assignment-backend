using BookstoreApplication.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolumesController : ControllerBase
    {
        private readonly IVolumeService _comicService;

        public VolumesController(IVolumeService comicService)
        {
            _comicService = comicService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFilteredVolumesByName([FromQuery] string query) 
        {
            return Ok(await _comicService.GetFilteredVolumesByNameAsync(query));  
        }

    }
}
