namespace Backend.Models
{
    using System.Text.Json.Serialization;

    public class FavoriteNovelDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("userID")]
        public int UserId { get; set; }

        [JsonPropertyName("novelID")]
        public int NovelId { get; set; }
    }

}
