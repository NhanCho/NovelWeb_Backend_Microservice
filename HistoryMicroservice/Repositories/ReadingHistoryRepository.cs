using MySql.Data.MySqlClient;
using Dapper;
using HistoryMicroservice.Models;

namespace HistoryMicroservice.Repositories
{
    public interface IReadingHistoryRepository
    {
        IEnumerable<ReadingHistory> GetByUserId(int userId);
        void AddOrUpdateHistory(ReadingHistory history);
    }

    public class ReadingHistoryRepository : IReadingHistoryRepository
    {
        private readonly MySqlConnection _connection;

        public ReadingHistoryRepository(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
        }

        public IEnumerable<ReadingHistory> GetByUserId(int userId)
        {
            var query = "SELECT * FROM ReadingHistory WHERE UserId = @UserId";
            return _connection.Query<ReadingHistory>(query, new { UserId = userId });
        }

        public void AddOrUpdateHistory(ReadingHistory history)
        {
            // Xóa bản ghi cũ nếu tồn tại
            var deleteQuery = "DELETE FROM ReadingHistory WHERE UserId = @UserId AND NovelId = @NovelId";
            _connection.Execute(deleteQuery, new { UserId = history.UserId, NovelId = history.NovelId });

            // Thêm bản ghi mới
            var insertQuery = @"
            INSERT INTO ReadingHistory (UserId, NovelId, ChapterID, LastReadDate)
            VALUES (@UserId, @NovelId, @ChapterID, @LastReadDate)";
            _connection.Execute(insertQuery, new
            {
                UserId = history.UserId,
                NovelId = history.NovelId,
                ChapterID = history.ChapterID,
                LastReadDate = DateTime.Now
            });
        }

    }
}