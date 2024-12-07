
namespace Backend.Services
{
    using Backend.Models;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using Microsoft.IdentityModel.Tokens;

    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UserService");
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var loginRequest = new { Email = email, Password = password };

            var response = await _httpClient.PostAsJsonAsync("api/User/Login", loginRequest);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Login failed: {error}");
            }

            // Đọc JSON object và trích xuất "token"
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            if (jsonResponse.TryGetProperty("token", out var tokenElement))
            {
                return tokenElement.GetString();
            }

            throw new HttpRequestException("Token not found in response.");
        }


        public int DecodeJwtAndGetUserId(string token)
        {
            Console.WriteLine($"Token received: {token}");

            // Kiểm tra nếu token là JSON object (chứa "token")
            if (token.StartsWith("{") && token.Contains("\"token\":"))
            {
                var jsonDoc = JsonDocument.Parse(token);
                if (jsonDoc.RootElement.TryGetProperty("token", out var tokenElement))
                {
                    token = tokenElement.GetString(); // Trích xuất giá trị "token"
                }
            }

            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
            {
                throw new SecurityTokenMalformedException("Token is not well-formed");
            }

            var jwtToken = handler.ReadJwtToken(token);
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        }



        public async Task<string> LogoutAsync(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/User/Logout");
            request.Headers.Add("Authorization", $"Bearer {token}");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Logout failed: {error}");
            }

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> UpdateProfileAsync(string token, string username, string email, string password)
        {
            var updateProfileRequest = new
            {
                Username = username,
                Email = email,
                Password = password
            };

            var request = new HttpRequestMessage(HttpMethod.Put, "api/User/UpdateProfile");
            request.Headers.Add("Authorization", $"Bearer {token}");
            request.Content = JsonContent.Create(updateProfileRequest);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Update profile failed: {error}");
            }

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var response = await _httpClient.GetAsync("/api/User/GetUsers");
            response.EnsureSuccessStatusCode();

            var users = await response.Content.ReadFromJsonAsync<IEnumerable<UserDto>>();
            return users ?? new List<UserDto>();
        }




        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/User/GetUserById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var user = await response.Content.ReadFromJsonAsync<UserDto>();
            return user;
        }

        public async Task<bool> CreateUserAsync(UserDto user)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/User/AddUser", user);

            // Ghi log trạng thái và nội dung phản hồi
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Status: {response.StatusCode}");
            Console.WriteLine($"Response Body: {responseBody}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error adding user: {responseBody}");
                return false;
            }

            // Trả về true nếu thành công
            return true;
        }




        public async Task<bool> UpdateUserAsync(int id, UserDto user)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/User/UpdateUser/{id}", user);

            // Ghi log trạng thái và nội dung phản hồi
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Status: {response.StatusCode}");
            Console.WriteLine($"Response Body: {responseBody}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error updating user: {responseBody}");
                return false;
            }

            // Trả về true nếu thành công
            return true;
        }




        public async Task<bool> DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/User/DeleteUser/{id}");
            return response.IsSuccessStatusCode;
        }

        // Lấy danh sách người dùng mà UserId đã theo dõi
        public async Task<IEnumerable<FollowedUserDto>> GetFollowedUsersAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"/api/User/GetFollowedUsers/{userId}");
            response.EnsureSuccessStatusCode();

            var followedUsers = await response.Content.ReadFromJsonAsync<IEnumerable<FollowedUserDto>>();
            return followedUsers ?? new List<FollowedUserDto>();
        }


        // Theo dõi một người dùng
        public async Task<bool> FollowUserAsync(int userId, int followUserId)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/User/FollowUser", new
            {
                userID = userId, // Key phải là userID (viết đúng như UserMicroservice yêu cầu)
                followedUserID = followUserId // Key phải là followedUserID
            });

            return response.IsSuccessStatusCode;
        }


        // Hủy theo dõi một người dùng
        public async Task<bool> UnfollowUserAsync(int userId, int followedUserId)
        {
            var response = await _httpClient.DeleteAsync($"/api/User/UnfollowUser/{userId}/{followedUserId}");
            return response.IsSuccessStatusCode;
        }

        // Lấy danh sách tiểu thuyết yêu thích
        public async Task<IEnumerable<FavoriteNovelDto>> GetFavoriteNovelsAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"/api/User/GetFavoriteNovels/{userId}");
            response.EnsureSuccessStatusCode();

            var favoriteNovels = await response.Content.ReadFromJsonAsync<IEnumerable<FavoriteNovelDto>>();
            return favoriteNovels ?? new List<FavoriteNovelDto>();
        }


        // Thêm tiểu thuyết vào danh sách yêu thích
        public async Task<bool> AddFavoriteNovelAsync(int userId, int novelId)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/User/AddFavoriteNovel", new
            {
                UserId = userId,
                NovelId = novelId
            });

            return response.IsSuccessStatusCode;
        }

        // Xóa tiểu thuyết khỏi danh sách yêu thích
        public async Task<bool> RemoveFavoriteNovelAsync(int userId, int novelId)
        {
            var response = await _httpClient.DeleteAsync($"/api/User/RemoveFavoriteNovel/{userId}/{novelId}");
            return response.IsSuccessStatusCode;
        }

    }

}
