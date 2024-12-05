using Microsoft.AspNetCore.Mvc;
using NovelMicroservice.Models;
using NovelMicroservice.Repositories;

namespace NovelMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        private readonly IChapterRepository _chapterRepository;

        public ChapterController(IChapterRepository chapterRepository)
        {
            _chapterRepository = chapterRepository;
        }

        // GET: api/Chapter/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetChapterByID(int id)
        {
            var chapter = await _chapterRepository.GetChapterByIDAsync(id);
            if (chapter == null)
            {
                return NotFound("Chapter not found.");
            }
            return Ok(chapter);
        }

        // GET: api/Chapter/Novel/{novelId}
        [HttpGet("Novel/{novelId}")]
        public async Task<IActionResult> GetChaptersByNovelID(int novelId)
        {
            var chapters = await _chapterRepository.GetChaptersByNovelIDAsync(novelId);
            return Ok(chapters);
        }

        // POST: api/Chapter
        [HttpPost]
        public async Task<IActionResult> AddChapter([FromBody] Chapter chapter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            await _chapterRepository.AddChapterAsync(chapter);
            return CreatedAtAction(nameof(GetChapterByID), new { id = chapter.ChapterID }, chapter);
        }

        // DELETE: api/Chapter/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChapter(int id)
        {
            await _chapterRepository.DeleteChapterAsync(id);
            return NoContent();
        }
    }
}
