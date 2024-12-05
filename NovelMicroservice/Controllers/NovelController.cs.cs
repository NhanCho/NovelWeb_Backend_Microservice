using Microsoft.AspNetCore.Mvc;
using NovelMicroservice.Models;
using NovelMicroservice.Repositories;

namespace NovelMicroservice.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NovelController : ControllerBase
{
    private readonly INovelRepository _repository;

    public NovelController(INovelRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var novels = await _repository.GetAllNovels();
        return Ok(novels);
    }

    [HttpGet("getbyid/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var novel = await _repository.GetNovelById(id);
        if (novel == null) return NotFound();
        return Ok(novel);
    }

    [HttpGet("getbycategoryid/{categoryId}")]
    public async Task<IActionResult> GetByCategoryId(int categoryId)
    {
        var novels = await _repository.GetNovelsByCategoryId(categoryId);
        return Ok(novels);
    }

    [HttpPost("post")]
    public async Task<IActionResult> Post(Novel novel)
    {
        await _repository.AddNovel(novel);
        return Ok();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteNovel(id);
        return Ok();
    }
}
