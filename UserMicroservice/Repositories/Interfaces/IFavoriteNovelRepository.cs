using UserMicroservice.Models;

namespace UserMicroservice.Repositories.Interfaces
{
    public interface IFavoriteNovelRepository
    {
        IEnumerable<FavoriteNovel> GetFavoriteNovelsByUserId(int userId);
        void AddFavoriteNovel(FavoriteNovel favoriteNovel);
        void RemoveFavoriteNovel(int userId, int novelId);
    }
}
