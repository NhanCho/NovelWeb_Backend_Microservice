using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class AddHistoryRequest
    {
        [JsonPropertyName("userID")]
        public int UserId { get; set; }

        [JsonPropertyName("NovelID")]
        public int NovelId { get; set; }

        [JsonPropertyName("ChapterID")]
        public int? ChapterID { get; set; } // Đổi từ LastReadChapterId thành ChapterID
    }
}
