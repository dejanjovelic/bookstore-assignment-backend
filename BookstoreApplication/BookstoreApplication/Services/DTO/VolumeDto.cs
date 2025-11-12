using System.Text.Json;
using System.Text.Json.Serialization;

namespace BookstoreApplication.Services.DTO
{
    public class VolumeDto
    {
        [JsonPropertyName("api_detail_url")]
        public required string ApiDetailUrl { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public ImageDto? Image { get; set; }

        [JsonPropertyName("count_of_issues")]
        public int CountOfIssues { get; set; }
    }
}
