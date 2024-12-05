namespace NovelMicroservice.Controllers;
using Microsoft.AspNetCore.Mvc;
using NovelMicroservice.Models;
using NovelMicroservice.Repositories;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _repository;

    public CategoryController(ICategoryRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _repository.GetAllCategories();
        return Ok(categories);
    }

    [HttpPost("post")]
    public async Task<IActionResult> Post(Category category)
    {
        await _repository.AddCategory(category);
        return Ok();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteCategory(id);
        return Ok();
    }
}
