using BookstoreApplication.Services.DTO;
using BookstoreApplication.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly IIssueService _issueService;

        public IssuesController(IIssueService issueService)
        {
            _issueService = issueService;
        }

        [HttpGet]
        public async Task<IActionResult> GetIssuesFromVolumeAsync([FromQuery] int volumeId)
        {
            return Ok(await _issueService.GetIssuesFromVolumeAsync(volumeId));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateIssueDataDto issueDataDto)
        {
            return Ok(await _issueService.CreateAsync(issueDataDto));
        }

    }
}
