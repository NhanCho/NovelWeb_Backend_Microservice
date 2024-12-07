using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class Notification
    {
        public int Id { get; set; }

        [JsonPropertyName("userID")]
        public int UserId { get; set; } // khớp với notification microservice
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; }
    }
}
