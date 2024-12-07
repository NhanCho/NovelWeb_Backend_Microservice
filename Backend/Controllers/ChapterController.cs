using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        private readonly IChapterService _chapterService;

        public ChapterController(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChapterById(int id)
        {
            var result = await _chapterService.GetChapterByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("Novel/{novelId}")]
        public async Task<IActionResult> GetChaptersByNovelId(int novelId)
        {
            var result = await _chapterService.GetChaptersByNovelIdAsync(novelId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddChapter([FromBody] Chapter chapter)
        {
            await _chapterService.AddChapterAsync(chapter);
            return CreatedAtAction(nameof(GetChapterById), new { id = chapter.ChapterID }, chapter);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChapter(int id)
        {
            await _chapterService.DeleteChapterAsync(id);
            return NoContent();
        }
    }
}
