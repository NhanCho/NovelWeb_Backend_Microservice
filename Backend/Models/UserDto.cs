using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class UserDto
    {
        [JsonPropertyName("userID")]
        public int UserID { get; set; } // Bắt buộc để khớp với JSON từ UserMicroservice

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }
    }



}
