using BookstoreApplication.Services.IServices;
using System.Text.Json;
using BookstoreApplication.Services.Exceptions;
using BookstoreApplication.Models.IRepositoies;
using AutoMapper;
using BookstoreApplication.Models;
using BookstoreApplication.Services.DTO;

namespace BookstoreApplication.Services
{
    public class IssueService : IIssueService
    {
        private readonly IConfiguration _configuration;
        private readonly IComicVineConnection _connection;
        private readonly ILogger<IssueService> _logger;
        private readonly IIssuesRepository _issuesRepository;
        private readonly IMapper _mapper;


        public IssueService(
            IConfiguration configuration,
            IComicVineConnection connection,
            ILogger<IssueService> logger,
            IIssuesRepository issuesRepository,
            IMapper mapper)
        {
            _configuration = configuration;
            _connection = connection;
            _logger = logger;
            _issuesRepository = issuesRepository;
            _mapper = mapper;
        }

        public async Task<List<IssueDto>> GetIssuesFromVolumeAsync(int volumeId)
        {
            var url = $"{_configuration["ComicVine:BaseUrl"]}/issues" +
                $"?api_key={_configuration["ComicVine:APIKey"]}" +
                $"&format=json" +
                $"&filter=volume:{Uri.EscapeDataString(volumeId.ToString())}";

            var json = await _connection.Get(url);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<IssueDto>>(json, options)!;
        }
        public async Task<Issue> CreateAsync(CreateIssueDataDto issueDataDto)
        {
            if (issueDataDto == null)
            {
                throw new BadRequestException("Invalid data.");
            }

            Issue issue = await _issuesRepository.GetByExternalId(issueDataDto.ExternalId);

            if (issue != null)
            {
                throw new ForbiddenException("Issue allready exist.");
            }

            return await _issuesRepository.CreateAsync(_mapper.Map<Issue>(issueDataDto));

        }
    }
}
