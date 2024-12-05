using CommentMicroservice.Models;

public class CommentFacade
{
    private readonly CommentRepository _repository;

    public CommentFacade()
    {
        _repository = new CommentRepository();
    }

    public List<Comment> GetCommentsByNovel(int novelId)
    {
        return _repository.GetCommentsByNovel(novelId);
    }

    public void AddComment(Comment comment)
    {
        _repository.AddComment(comment);
    }

    public void UpdateComment(int id, Comment comment)
    {
        _repository.UpdateComment(id, comment);
    }

    public void DeleteComment(int id)
    {
        _repository.DeleteComment(id);
    }
}
