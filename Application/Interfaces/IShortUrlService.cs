namespace ChameleonCoreAPI.Application
{
    public interface IShortUrlService
    {
        Task<string> CreateShortUrl(string originalUrl);
        Task<string?> GetOriginalUrl(string shortCode);
    }
}