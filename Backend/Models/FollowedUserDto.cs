namespace Backend.Models
{
    using System.Text.Json.Serialization;

    public class FollowedUserDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("userID")]
        public int userID { get; set; }

        [JsonPropertyName("followedUserID")]
        public int followedUserID { get; set; }
    }

}
