namespace Backend.Services
{
    public class HistoryService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HistoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
