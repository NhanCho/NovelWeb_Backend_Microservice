namespace Backend.Services
{
    public class NotificationService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NotificationService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
