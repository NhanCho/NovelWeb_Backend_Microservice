using Backend.Models;
using System.Net.Http.Json;

namespace Backend.Services
{
    public interface IHistoryService
    {
        Task AddOrUpdateReadingHistory(AddHistoryRequest request);
        Task<IEnumerable<ReadingHistory>> GetReadingHistoryByUserId(int userId);
    }

    public class HistoryService : IHistoryService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _userHttpClient;
        private readonly HttpClient _novelHttpClient;

        public HistoryService(HttpClient httpClient, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5005/"); // Đường dẫn microservice History
            _userHttpClient = httpClientFactory.CreateClient("UserService");
            _novelHttpClient = httpClientFactory.CreateClient("NovelService");
        }

        /// Thêm hoặc cập nhật lịch sử đọc.
        public async Task AddOrUpdateReadingHistory(AddHistoryRequest request)
        {
            // Kiểm tra UserId qua UserService
            var userResponse = await _userHttpClient.GetAsync($"api/User/GetUserById/{request.UserId}");
            if (!userResponse.IsSuccessStatusCode)
            {
                throw new Exception($"UserId {request.UserId} không tồn tại!");
            }

            //var novelResponse = await _novelHttpClient.GetAsync($"api/Novel/getbyid/{request.NovelId}");
            //if (!novelResponse.IsSuccessStatusCode)
            //{
            //    throw new Exception($"NovelId {request.NovelId} không tồn tại!");
            //}

            // Gửi yêu cầu POST tới API của History
            var response = await _httpClient.PostAsJsonAsync("api/ReadingHistory", request);
            response.EnsureSuccessStatusCode();
        }

        /// Lấy lịch sử đọc theo UserId.
        public async Task<IEnumerable<ReadingHistory>> GetReadingHistoryByUserId(int userId)
        {
            // Kiểm tra UserId qua UserService
            var userResponse = await _userHttpClient.GetAsync($"api/User/GetUserById/{userId}");
            if (!userResponse.IsSuccessStatusCode)
            {
                throw new Exception($"UserId {userId} không tồn tại!");
            }

            // Gửi yêu cầu GET tới API của History
            var response = await _httpClient.GetAsync($"api/ReadingHistory/{userId}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<IEnumerable<ReadingHistory>>()
                   ?? new List<ReadingHistory>();
        }
    }
}
