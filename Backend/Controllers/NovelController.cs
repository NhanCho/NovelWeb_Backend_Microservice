using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
[Route("api/[controller]")]
[ApiController]
public class NovelController : ControllerBase
{
    private readonly INovelService _novelService;

    public NovelController(INovelService novelService)
    {
        _novelService = novelService;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllNovels()
    {
        var result = await _novelService.GetAllNovelsAsync();
        return Ok(result);
    }

    [HttpGet("getbyid/{id}")]
    public async Task<IActionResult> GetNovelById(int id)
    {
        var result = await _novelService.GetNovelByIdAsync(id);
        return Ok(result);
    }

    [HttpPost("post")] // API mới
    public async Task<IActionResult> AddNovel([FromBody] Novel novel)
    {
        await _novelService.AddNovelAsync(novel);
        return Ok("Novel added successfully");
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteNovel(int id)
    {
        await _novelService.DeleteNovelAsync(id);
        return NoContent();
    }

    [HttpGet("getbycategoryid/{categoryId}")]
    public async Task<IActionResult> GetNovelsByCategoryId(int categoryId)
    {
        var result = await _novelService.GetNovelsByCategoryIdAsync(categoryId);
        return Ok(result);
    }

    [HttpGet("getbyname/{name}")]
    public async Task<IActionResult> GetNovelByName(string name)
    {
        var novel = await _novelService.GetNovelByNameAsync(name);
        if (novel == null)
        {
            return NotFound("Novel not found.");
        }
        return Ok(novel);
    }

}
