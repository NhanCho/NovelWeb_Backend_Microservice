using UserMicroservice.Data;
using UserMicroservice.Models;
using UserMicroservice.Repositories.Interfaces;

namespace UserMicroservice.Repositories.Implementations
{
    public class FavoriteNovelRepository : IFavoriteNovelRepository
    {
        private readonly UserDbContext _context;

        public FavoriteNovelRepository(UserDbContext context)
        {
            _context = context;
        }

        public IEnumerable<FavoriteNovel> GetFavoriteNovelsByUserId(int userId)
        {
            return _context.FavoriteNovels.Where(f => f.UserID == userId).ToList();
        }

        public void AddFavoriteNovel(FavoriteNovel favoriteNovel)
        {
            _context.FavoriteNovels.Add(favoriteNovel);
            _context.SaveChanges();
        }

        public void RemoveFavoriteNovel(int userId, int novelId)
        {
            var favoriteNovel = _context.FavoriteNovels
                .FirstOrDefault(f => f.UserID == userId && f.NovelID == novelId);

            if (favoriteNovel != null)
            {
                _context.FavoriteNovels.Remove(favoriteNovel);
                _context.SaveChanges();
            }
        }
    }
}
