using NovelMicroservice.Models;

namespace NovelMicroservice.Repositories
{
    public interface IChapterRepository
    {
        Task<Chapter> GetChapterByIDAsync(int id);
        Task<List<Chapter>> GetChaptersByNovelIDAsync(int novelId);
        Task AddChapterAsync(Chapter chapter);
        Task DeleteChapterAsync(int id);
    }
}
