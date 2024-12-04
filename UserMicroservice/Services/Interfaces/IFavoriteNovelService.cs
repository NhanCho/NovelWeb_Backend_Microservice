using UserMicroservice.Models;

namespace UserMicroservice.Services.Interfaces
{
    public interface IFavoriteNovelService
    {
        IEnumerable<FavoriteNovel> GetFavoriteNovelsByUserId(int userId);
        void AddFavoriteNovel(FavoriteNovel favoriteNovel);
        void RemoveFavoriteNovel(int userId, int novelId);
    }
}
