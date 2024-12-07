using Microsoft.EntityFrameworkCore;
using NovelMicroservice.Models;
using NovelMicroservice.Repositories;

namespace NovelMicroservice.Repositoriesl;
using Microsoft.EntityFrameworkCore;
using NovelMicroservice.Data;
using NovelMicroservice.Models;

public class NovelRepository : INovelRepository
{
    private readonly AppDbContext _context;

    public NovelRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Novel>> GetAllNovels()
    {
        return await _context.Novels.ToListAsync();  // Trả về tất cả các tiểu thuyết mà không cần thông tin về Category
    }

    public async Task<Novel> GetNovelById(int id)
    {
        return await _context.Novels.FindAsync(id);  // Trả về tiểu thuyết theo id
    }

    public async Task AddNovel(Novel novel)
    {
        await _context.Novels.AddAsync(novel);
        await _context.SaveChangesAsync();
    }
    public async Task<Novel> GetNovelByNameAsync(string name)
    {
        return await _context.Novels.FirstOrDefaultAsync(n => n.Name == name);
    }
    public async Task DeleteNovel(int id)
    {
        var novel = await _context.Novels.FindAsync(id);
        if (novel != null)
        {
            _context.Novels.Remove(novel);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Novel>> GetNovelsByCategoryId(int categoryId)
    {
        return await _context.Novels
            .Where(n => n.CategoryID == categoryId)  // Lọc các tiểu thuyết theo CategoryID
            .ToListAsync();
    }
}
