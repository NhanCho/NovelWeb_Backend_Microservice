using Microsoft.EntityFrameworkCore;
using NovelMicroservice.Data;
using NovelMicroservice.Models;

namespace NovelMicroservice.Repositories
{
    public class ChapterRepository : IChapterRepository
    {
        private readonly AppDbContext _context;

        public ChapterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Chapter> GetChapterByIDAsync(int id)
        {
            return await _context.Chapters.FirstOrDefaultAsync(c => c.ChapterID == id);
        }

        public async Task<List<Chapter>> GetChaptersByNovelIDAsync(int novelId)
        {
            return await _context.Chapters.Where(c => c.NovelID == novelId).ToListAsync();
        }

        public async Task AddChapterAsync(Chapter chapter)
        {
            await _context.Chapters.AddAsync(chapter);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteChapterAsync(int id)
        {
            var chapter = await _context.Chapters.FirstOrDefaultAsync(c => c.ChapterID == id);
            if (chapter != null)
            {
                _context.Chapters.Remove(chapter);
                await _context.SaveChangesAsync();
            }
        }
    }
}
