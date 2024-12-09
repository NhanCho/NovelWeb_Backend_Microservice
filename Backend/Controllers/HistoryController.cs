using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly HistoryService _historyService;

        public HistoryController(HistoryService historyService)
        {
            _historyService = historyService;
        }

        // POST: Thêm hoặc cập nhật lịch sử đọc
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateReadingHistory([FromBody] AddHistoryRequest request)
        {
            try
            {
                await _historyService.AddOrUpdateReadingHistory(request);
                return Ok(new { message = "Reading history updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // GET: Lấy danh sách lịch sử đọc theo UserId
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetReadingHistoryByUserId(int userId)
        {
            try
            {
                var histories = await _historyService.GetReadingHistoryByUserId(userId);
                return Ok(histories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
