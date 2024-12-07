using Backend.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Backend.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByNameAsync(string name);
        Task AddCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
    }



    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public CategoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CategoryService");
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("/api/Category/getall");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(content);
                return categories;
            }

            return new List<Category>();
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            var response = await _httpClient.GetAsync($"/api/Category/getbyname/{name}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var category = JsonConvert.DeserializeObject<Category>(content);
                return category;
            }

            return null;
        }

        public async Task AddCategoryAsync(Category category)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Category/post", category);

            if (!response.IsSuccessStatusCode)
            {
                throw new System.Exception("Failed to add category.");
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Category/delete/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new System.Exception("Failed to delete category.");
            }
        }
    }
}
