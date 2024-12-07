using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class NotificationRequest
    {
        public string Message { get; set; }

        [JsonPropertyName("userID")]
        public int UserId { get; set; }
    }
}
