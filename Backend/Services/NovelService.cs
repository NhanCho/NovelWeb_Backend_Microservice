namespace Backend.Services
{
    public class NovelService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NovelService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
