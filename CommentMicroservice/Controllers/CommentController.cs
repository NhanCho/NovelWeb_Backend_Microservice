using CommentMicroservice.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/comments")]
public class CommentController : ControllerBase
{
    private readonly CommentFacade _facade;

    public CommentController()
    {
        _facade = new CommentFacade();
    }

    [HttpGet("{novelId}")]
    public IActionResult GetComments(int novelId)
    {
        var comments = _facade.GetCommentsByNovel(novelId);
        return Ok(comments);
    }

    [HttpPost]
    public IActionResult AddComment([FromBody] Comment comment)
    {
        Console.WriteLine($"CreatedDate: {comment.CreatedDate}");
        _facade.AddComment(comment);
        return Created("", comment);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateComment(int id, [FromBody] Comment comment)
    {
        _facade.UpdateComment(id, comment);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteComment(int id)
    {
        _facade.DeleteComment(id);
        return NoContent();
    }
}
