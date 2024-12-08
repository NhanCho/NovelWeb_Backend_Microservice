using Backend.Models;
using System.Text.Json;

namespace Backend.Services
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetCommentsByNovelAsync(int novelId);
        Task<CommentDto> AddCommentAsync(CommentDto comment);
        Task UpdateCommentAsync(int id, CommentDto comment);
        Task DeleteCommentAsync(int id);
        Task<string?> GetUserNameByIdAsync(int userId); // Thêm method lấy tên user
    }

    public class CommentService : ICommentService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _userHttpClient; // HttpClient để gọi UserService

        public CommentService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CommentService");
            _userHttpClient = httpClientFactory.CreateClient("UserService");
        }

        public async Task<List<CommentDto>> GetCommentsByNovelAsync(int novelId)
        {
            var comments = await _httpClient.GetAsync($"api/comments/{novelId}");
            comments.EnsureSuccessStatusCode();

            var commentList = await comments.Content.ReadFromJsonAsync<List<CommentDto>>() ?? new List<CommentDto>();

            // Debugging: Log the comments fetched before adding UserName
            Console.WriteLine($"Fetched {commentList.Count} comments for Novel ID {novelId}");

            foreach (var comment in commentList)
            {
                try
                {
                    var userResponse = await _userHttpClient.GetAsync($"api/User/GetUserById/{comment.UserID}");
                    if (userResponse.IsSuccessStatusCode)
                    {
                        var user = await userResponse.Content.ReadFromJsonAsync<UserDto>();
                        if (user != null)
                        {
                            comment.UserName = user.Username; // Gắn UserName vào comment
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching User with ID: {comment.UserID}. Exception: {ex.Message}");
                }
            }

            return commentList;
        }


        public async Task<CommentDto> AddCommentAsync(CommentDto comment)
        {
            // Debug: Kiểm tra trước khi lưu vào DB
            Console.WriteLine($"UserName: {comment.UserName}");

            if (string.IsNullOrEmpty(comment.UserName))
            {
                var userName = await GetUserNameByIdAsync(comment.UserID);
                comment.UserName = userName ?? "Anonymous";
            }

            // Lưu comment vào cơ sở dữ liệu hoặc backend
            var response = await _httpClient.PostAsJsonAsync("api/comments", comment);
            response.EnsureSuccessStatusCode();

            var createdComment = await response.Content.ReadFromJsonAsync<CommentDto>();

            // Đảm bảo rằng UserName được truyền về trong response
            createdComment.UserName = comment.UserName; // Gán lại UserName từ comment

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

        public async Task<string?> GetUserNameByIdAsync(int userId)
        {
            var response = await _userHttpClient.GetAsync($"api/User/GetUserById/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get user: UserId {userId}");
                return null;
            }

            var user = await response.Content.ReadFromJsonAsync<UserDto>();
            Console.WriteLine($"Fetched user: {user?.Username} for UserId {userId}");
            return user?.Username;
        }
    }
}
