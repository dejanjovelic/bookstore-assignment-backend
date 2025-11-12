using BookstoreApplication.Services.IServices;
using BookstoreApplication.Controllers;
using System.Text.Json;
using BookstoreApplication.Services.DTO;

namespace BookstoreApplication.Services
{
    public class VolumeService : IVolumeService
    {
        private readonly IConfiguration _configuration;
        private readonly IComicVineConnection _comicVineConnection;

        public VolumeService(IConfiguration configuration, IComicVineConnection comicVineConnection)
        {
            _configuration = configuration;
            _comicVineConnection = comicVineConnection;
        }

        public async Task<List<VolumeDto>> GetFilteredVolumesByNameAsync(string query)
        {
            var url = $"{_configuration["ComicVine:BaseUrl"]}/volumes" +
                $"?api_key={_configuration["ComicVine:APIKey"]}" +
                $"&format=json" +
                $"&filter=name:{Uri.EscapeDataString(query)}";

            var json = await _comicVineConnection.Get(url);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<VolumeDto>>(json, options)!;
        }
    }
}
