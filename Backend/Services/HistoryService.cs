using Backend.Models;
using System.Net.Http.Json;

namespace Backend.Services
{
    public class HistoryService
    {
        private readonly HttpClient _httpClient;

        public HistoryService(HttpClient httpClient)
        {
            // Inject HttpClient từ Dependency Injection
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5005/"); // Đường dẫn microservice
        }

        // Thêm hoặc cập nhật lịch sử đọc
        public async Task AddOrUpdateReadingHistory(AddHistoryRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/ReadingHistory", request);

                // Kiểm tra nếu request không thành công
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                // Log lỗi
                Console.WriteLine($"Error in AddOrUpdateReadingHistory: {ex.Message}");
                throw;
            }
        }

        // Lấy danh sách lịch sử đọc theo UserId
        public async Task<IEnumerable<ReadingHistory>> GetReadingHistoryByUserId(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/ReadingHistory/{userId}");

                // Kiểm tra nếu request không thành công
                response.EnsureSuccessStatusCode();

                // Deserialize JSON thành danh sách ReadingHistory
                return await response.Content.ReadFromJsonAsync<IEnumerable<ReadingHistory>>();
            }
            catch (Exception ex)
            {
                // Log lỗi
                Console.WriteLine($"Error in GetReadingHistoryByUserId: {ex.Message}");
                throw;
            }
        }
    }
}
