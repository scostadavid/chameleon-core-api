namespace ChameleonCoreAPI.Domain
{
    public interface IShortUrlRepository
    {
        Task<ShortUrl?> GetByShortCode(string shortCode);
        Task Create(ShortUrl shortUrl);
        Task IncrementAccessCount(string shortCode);
    }
}
