using Microsoft.EntityFrameworkCore;
using ChameleonCoreAPI.Domain;
using System.Threading.Tasks;

namespace ChameleonCoreAPI.Infrastructure
{
    public class ShortUrlRepository : IShortUrlRepository
    {
        private readonly AppDbContext _context;

        public ShortUrlRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ShortUrl?> GetByShortCode(string shortCode)
        {
            return await _context.ShortUrls.FirstOrDefaultAsync(s => s.ShortCode == shortCode);
        }

        public async Task Create(ShortUrl shortUrl)
        {
            await _context.ShortUrls.AddAsync(shortUrl);
            await _context.SaveChangesAsync();
        }

        public async Task IncrementAccessCount(string shortCode)
        {
            var shortUrl = await _context.ShortUrls.FirstOrDefaultAsync(s => s.ShortCode == shortCode);
            if (shortUrl != null)
            {
                shortUrl.AccessCount++;
                await _context.SaveChangesAsync();
            }
        }
    }
}
