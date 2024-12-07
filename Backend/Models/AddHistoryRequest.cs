using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class AddHistoryRequest
    {
        [JsonPropertyName("userID")]
        public int UserId { get; set; }
        public int NovelId { get; set; }
        public int? LastReadChapterId { get; set; }
    }
}
