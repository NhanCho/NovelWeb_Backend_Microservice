using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Backend.Models;
using System.Text;

namespace Backend.Services
{
    public interface INovelService
    {
        Task<List<Novel>> GetAllNovelsAsync();
        Task<Novel> GetNovelByIdAsync(int id);
        Task AddNovelAsync(Novel novel); // Thêm API mới
        Task DeleteNovelAsync(int id);
        Task<List<Novel>> GetNovelsByCategoryIdAsync(int categoryId);
        Task<Novel> GetNovelByNameAsync(string name); // Thêm API mới
    }

    public class NovelService : INovelService
    {
        private readonly HttpClient _httpClient;
        public NovelService(HttpClient httpClient)
        {
            _httpClient = httpClient; // HttpClient sẽ được injected tự động
        }

        public NovelService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("NovelService");
        }

        public async Task<List<Novel>> GetAllNovelsAsync()
        {
            var response = await _httpClient.GetAsync("/api/Novel/getall");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Novel>>(content);
            }
            return new List<Novel>();
        }

        public async Task<Novel> GetNovelByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Novel/getbyid/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Novel>(content);
            }
            return null;
        }

        public async Task AddNovelAsync(Novel novel) // API mới
        {
            var content = new StringContent(JsonConvert.SerializeObject(novel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Novel/post", content);

            if (!response.IsSuccessStatusCode)
            {
                // Xử lý lỗi nếu cần
                throw new Exception("Failed to add novel.");
            }
        }

        public async Task DeleteNovelAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Novel/delete/{id}");
            if (!response.IsSuccessStatusCode)
            {
                // Xử lý lỗi nếu cần
                throw new Exception("Failed to delete novel.");
            }
        }

        public async Task<List<Novel>> GetNovelsByCategoryIdAsync(int categoryId)
        {
            var response = await _httpClient.GetAsync($"/api/Novel/getbycategoryid/{categoryId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Novel>>(content);
            }
            return new List<Novel>();
        }

        public async Task<Novel> GetNovelByNameAsync(string name)
        {
            var response = await _httpClient.GetAsync($"/api/novel/getbyname/{name}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var novel = JsonConvert.DeserializeObject<Novel>(content);
                return novel;
            }
            return null;
        }

    }
}
