using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class ReadingHistory
    {
        public int Id { get; set; }

        [JsonPropertyName("userID")]
        public int UserId { get; set; }

        [JsonPropertyName("NovelID")]
        public int NovelId { get; set; }

        [JsonPropertyName("ChapterID")]
        public int? ChapterID { get; set; } // Đổi từ LastReadChapterId thành ChapterID

        public DateTime LastReadDate { get; set; }
    }
}
