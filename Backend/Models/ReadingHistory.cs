using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class ReadingHistory
    {
        public int Id { get; set; }
        [JsonPropertyName("userID")]
        public int UserId { get; set; }
        public int NovelId { get; set; }
        public int? LastReadChapterId { get; set; }
        public DateTime LastReadDate { get; set; }
    }
}
