using NovelMicroservice.Models;

namespace NovelMicroservice.Repositories;
public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllCategories();
    Task<Category> GetCategoryById(int id);
    Task AddCategory(Category category);
    Task DeleteCategory(int id);
    Task<Category> GetCategoryByNameAsync(string name);
    Task UpdateCategoryImageAsync(int categoryID, string imageUrl);

}
