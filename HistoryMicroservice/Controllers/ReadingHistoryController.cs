using HistoryMicroservice.Models;
using HistoryMicroservice.Services;
using Microsoft.AspNetCore.Mvc;

namespace HistoryMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReadingHistoryController : ControllerBase
    {
        private readonly IReadingHistoryService _service;

        public ReadingHistoryController(IReadingHistoryService service)
        {
            _service = service;
        }

        [HttpGet("{userId}")]
        public IActionResult GetReadingHistory(int userId)
        {
            var history = _service.GetReadingHistory(userId);
            return Ok(history);
        }

        [HttpPost]
        public IActionResult AddOrUpdateReadingHistory([FromBody] AddHistoryRequest request)
        {
            _service.AddOrUpdateReadingHistory(request);
            return NoContent();
        }
    }

}
