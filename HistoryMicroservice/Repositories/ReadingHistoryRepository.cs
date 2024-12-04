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
            var query = @"
            INSERT INTO ReadingHistory (UserId, NovelId, LastReadChapterId, LastReadDate)
            VALUES (@UserId, @NovelId, @LastReadChapterId, @LastReadDate)
            ON DUPLICATE KEY UPDATE
            LastReadChapterId = @LastReadChapterId, LastReadDate = @LastReadDate";
            _connection.Execute(query, history);
        }
    }

}
