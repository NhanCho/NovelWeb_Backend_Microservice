using UserMicroservice.Models;
using UserMicroservice.Repositories.Interfaces;
using UserMicroservice.Services.Interfaces;

namespace UserMicroservice.Services.Implementations
{
    public class FavoriteNovelService : IFavoriteNovelService
    {
        private readonly IFavoriteNovelRepository _favoriteNovelRepository;

        public FavoriteNovelService(IFavoriteNovelRepository favoriteNovelRepository)
        {
            _favoriteNovelRepository = favoriteNovelRepository;
        }

        public IEnumerable<FavoriteNovel> GetFavoriteNovelsByUserId(int userId)
        {
            return _favoriteNovelRepository.GetFavoriteNovelsByUserId(userId);
        }

        public void AddFavoriteNovel(FavoriteNovel favoriteNovel)
        {
            _favoriteNovelRepository.AddFavoriteNovel(favoriteNovel);
        }

        public void RemoveFavoriteNovel(int userId, int novelId)
        {
            _favoriteNovelRepository.RemoveFavoriteNovel(userId, novelId);
        }
    }
}
