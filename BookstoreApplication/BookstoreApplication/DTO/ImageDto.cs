using System.Text.Json.Serialization;

namespace BookstoreApplication.DTO
{
    public class ImageDto
    {
        [JsonPropertyName("original_url")]
        public string? OriginalUrl { get; set; }

        [JsonPropertyName("tiny_url")]
        public string? TinyUrl { get; set; }

        [JsonPropertyName("small_url")]
        public string? SmallUrl { get; set; }

        [JsonPropertyName("medium_url")]
        public string? MediumUrl { get; set; }

        [JsonPropertyName("screen_url")]
        public string? ScreenUrl { get; set; }
    }
}
