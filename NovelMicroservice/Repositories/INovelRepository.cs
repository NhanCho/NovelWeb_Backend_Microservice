using NovelMicroservice.Models;

namespace NovelMicroservice.Repositories;

public interface INovelRepository
{
    Task<IEnumerable<Novel>> GetAllNovels();
    Task<Novel> GetNovelById(int id);
    Task AddNovel(Novel novel);
    Task DeleteNovel(int id);
    Task<IEnumerable<Novel>> GetNovelsByCategoryId(int categoryId);
    Task<Novel> GetNovelByNameAsync(string name);

}
