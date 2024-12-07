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
            // Gửi yêu cầu đến Comment API để lấy danh sách comment
            var response = await _httpClient.GetAsync($"api/comments/{novelId}");
            response.EnsureSuccessStatusCode();

            // Deserialize phản hồi từ API thành danh sách CommentDto
            var comments = await response.Content.ReadFromJsonAsync<List<CommentDto>>() ?? new List<CommentDto>();

            // Ghi log và kết nối với UserService để lấy thông tin User
            foreach (var comment in comments)
            {
                try
                {
                    // Gửi yêu cầu đến UserService để lấy thông tin người dùng
                    var userResponse = await _userHttpClient.GetAsync($"api/User/GetUserById/{comment.UserID}");
                    if (userResponse.IsSuccessStatusCode)
                    {
                        // Deserialize phản hồi từ UserService thành UserDto
                        var user = await userResponse.Content.ReadFromJsonAsync<UserDto>();
                        if (user != null)
                        {
                            // Ghi log thông tin comment và username
                            Console.WriteLine($"Comment ID: {comment.CommentID}, User: {user.Username}, Content: {comment.Content}");
                        }
                        else
                        {
                            Console.WriteLine($"User not found for UserID: {comment.UserID}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to fetch User with ID: {comment.UserID}. StatusCode: {userResponse.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching User with ID: {comment.UserID}. Exception: {ex.Message}");
                }
            }

            return comments;
        }


        public async Task<CommentDto> AddCommentAsync(CommentDto comment)
        {
            var response = await _httpClient.PostAsJsonAsync("api/comments", comment);
            response.EnsureSuccessStatusCode();

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
