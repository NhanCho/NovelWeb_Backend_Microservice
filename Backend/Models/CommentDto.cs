using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class CommentDto
    {
        public int CommentID { get; set; }
        public int NovelID { get; set; }

        [JsonPropertyName("userID")] // Nếu API dùng "userID" thay vì "UserID"
        public int UserID { get; set; }

        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        // Thêm UserName để lưu tên người dùng
        public string? UserName { get; set; }
    }
}
