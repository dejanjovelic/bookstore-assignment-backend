using System.Text.Json.Serialization;

namespace BookstoreApplication.DTO
{
    public class IssueDto
    {
        [JsonPropertyName("api_detail_url")]
        public string ApiDetailUrl { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public ImageDto? Image { get; set; }
        public string? Description { get; set; }

        [JsonPropertyName("issue_number")]
        public string IssueNumber { get; set; }

        [JsonPropertyName("cover_date")]
        public DateTime CoverDate { get; set; }

    }
}
