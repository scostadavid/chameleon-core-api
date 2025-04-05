using ChameleonCoreAPI.Domain;

namespace ChameleonCoreAPI.Application
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly IShortUrlRepository _repository;

        public ShortUrlService(IShortUrlRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> CreateShortUrl(string originalUrl)
        {
            var shortCode = Guid.NewGuid().ToString()[..6]; 
            var shortUrl = new ShortUrl { OriginalUrl = originalUrl, ShortCode = shortCode };
            await _repository.Create(shortUrl);
            return shortCode;
        }

        public async Task<string?> GetOriginalUrl(string shortCode)
        {
            var shortUrl = await _repository.GetByShortCode(shortCode);
            if (shortUrl != null)
            {
                await _repository.IncrementAccessCount(shortCode);
                return shortUrl.OriginalUrl;
            }
            return null;
        }
    }

}