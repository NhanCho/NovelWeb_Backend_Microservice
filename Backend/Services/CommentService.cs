using Backend.Models;

namespace Backend.Services
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetCommentsByNovelAsync(int novelId);
        Task<CommentDto> AddCommentAsync(CommentDto comment);
        Task UpdateCommentAsync(int id, CommentDto comment);
        Task DeleteCommentAsync(int id);
    }
    public class CommentService : ICommentService
    {
        private readonly HttpClient _httpClient;

        public CommentService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CommentService");
        }

        public async Task<List<CommentDto>> GetCommentsByNovelAsync(int novelId)
        {
            var response = await _httpClient.GetAsync($"api/comments/{novelId}");
            response.EnsureSuccessStatusCode();

            // Đảm bảo phản hồi được deserialize đúng cách
            var comments = await response.Content.ReadFromJsonAsync<List<CommentDto>>();
            return comments ?? new List<CommentDto>();
        }

        public async Task<CommentDto> AddCommentAsync(CommentDto comment)
        {
            var response = await _httpClient.PostAsJsonAsync("api/comments", comment);
            response.EnsureSuccessStatusCode();

            // Deserialize phản hồi từ Microservice
            var createdComment = await response.Content.ReadFromJsonAsync<CommentDto>();
            return createdComment ?? new CommentDto();
        }

        public async Task UpdateCommentAsync(int id, CommentDto comment)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/comments/{id}", comment);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCommentAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/comments/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}

