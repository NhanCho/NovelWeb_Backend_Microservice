using MySql.Data.MySqlClient;
using CommentMicroservice.Models;

public class CommentRepository
{
    private readonly MySqlConnection _connection;

    public CommentRepository()
    {
        _connection = DatabaseConnection.GetInstance().GetConnection();
    }

    public List<Comment> GetCommentsByNovel(int novelId)
    {
        var comments = new List<Comment>();
        string query = "SELECT * FROM Comments WHERE NovelID = @novelId";
        var cmd = new MySqlCommand(query, _connection);
        cmd.Parameters.AddWithValue("@novelId", novelId);

        _connection.Open();
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            comments.Add(new Comment
            {
                CommentID = reader.GetInt32("CommentID"),
                NovelID = reader.GetInt32("NovelID"),
                UserID = reader.GetInt32("UserID"),
                Content = reader.GetString("Content"),
                CreatedDate = reader.GetDateTime("CreatedDate")
            });
        }
        _connection.Close();

        return comments;
    }

    public void AddComment(Comment comment)
    {
        if (comment.CreatedDate == DateTime.MinValue)  // Kiểm tra nếu CreatedDate chưa được gán
        {
            comment.CreatedDate = DateTime.Now;  // Gán thời gian hiện tại
        }

        // Chỉ cần chèn NovelID, UserID, và Content. Không cần truyền CreatedDate vì MySQL sẽ tự động xử lý.
        string query = "INSERT INTO Comments (NovelID, UserID, Content, CreatedDate) VALUES (@novelId, @userId, @content, @createdDate)";
        var cmd = new MySqlCommand(query, _connection);
        cmd.Parameters.AddWithValue("@novelId", comment.NovelID);
        cmd.Parameters.AddWithValue("@userId", comment.UserID);
        cmd.Parameters.AddWithValue("@content", comment.Content);
        cmd.Parameters.AddWithValue("@createdDate", comment.CreatedDate);

        _connection.Open();
        cmd.ExecuteNonQuery();

        // Lấy CommentID sau khi insert
        cmd.CommandText = "SELECT LAST_INSERT_ID()";  // Lấy ID mới tạo từ MySQL
        comment.CommentID = Convert.ToInt32(cmd.ExecuteScalar());  // Gán giá trị CommentID

        _connection.Close();
    }



    public void UpdateComment(int id, Comment comment)
    {
        string query = "UPDATE Comments SET Content = @content WHERE CommentID = @id";
        var cmd = new MySqlCommand(query, _connection);
        cmd.Parameters.AddWithValue("@content", comment.Content);
        cmd.Parameters.AddWithValue("@id", id);

        _connection.Open();
        cmd.ExecuteNonQuery();
        _connection.Close();
    }

    public void DeleteComment(int id)
    {
        string query = "DELETE FROM Comments WHERE CommentID = @id";
        var cmd = new MySqlCommand(query, _connection);
        cmd.Parameters.AddWithValue("@id", id);

        _connection.Open();
        cmd.ExecuteNonQuery();
        _connection.Close();
    }
}
