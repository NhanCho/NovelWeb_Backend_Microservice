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

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetReadingHistoryByUserId(int userId)
        {
            try
            {
                var history = await _historyService.GetReadingHistoryByUserId(userId);
                return Ok(history);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
