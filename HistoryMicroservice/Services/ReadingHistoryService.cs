using HistoryMicroservice.Models;
using HistoryMicroservice.Repositories;

namespace HistoryMicroservice.Services
{
    public interface IReadingHistoryService
    {
        IEnumerable<ReadingHistory> GetReadingHistory(int userId);
        void AddOrUpdateReadingHistory(AddHistoryRequest request);
    }

    public class ReadingHistoryService : IReadingHistoryService
    {
        private readonly IReadingHistoryRepository _repository;

        public ReadingHistoryService(IReadingHistoryRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ReadingHistory> GetReadingHistory(int userId)
        {
            return _repository.GetByUserId(userId);
        }

        public void AddOrUpdateReadingHistory(AddHistoryRequest request)
        {
            var history = new ReadingHistory
            {
                UserId = request.UserId,
                NovelId = request.NovelId,
                ChapterID = request.ChapterID,
                LastReadDate = DateTime.Now
            };
            _repository.AddOrUpdateHistory(history);
        }
    }

}
