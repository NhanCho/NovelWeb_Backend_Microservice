using Backend.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Backend.Services
{
    public interface IChapterService
    {
        Task<Chapter> GetChapterByIdAsync(int id);
        Task<IEnumerable<Chapter>> GetChaptersByNovelIdAsync(int novelId);
        Task AddChapterAsync(Chapter chapter);
        Task DeleteChapterAsync(int id);
    }


    public class ChapterService : IChapterService
    {
        private readonly HttpClient _httpClient;

        public ChapterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public ChapterService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ChapterService");
        }

        public async Task<Chapter> GetChapterByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Chapter/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var chapter = JsonConvert.DeserializeObject<Chapter>(content);
                return chapter;
            }

            return null;
        }

        public async Task<IEnumerable<Chapter>> GetChaptersByNovelIdAsync(int novelId)
        {
            var response = await _httpClient.GetAsync($"/api/Chapter/Novel/{novelId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var chapters = JsonConvert.DeserializeObject<IEnumerable<Chapter>>(content);
                return chapters;
            }

            return new List<Chapter>();
        }

        public async Task AddChapterAsync(Chapter chapter)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Chapter", chapter);

            if (!response.IsSuccessStatusCode)
            {
                throw new System.Exception("Failed to add chapter.");
            }
        }

        public async Task DeleteChapterAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Chapter/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new System.Exception("Failed to delete chapter.");
            }
        }
    }
}
