using Microsoft.AspNetCore.Mvc;
using ChameleonCoreAPI.Application;
using ChameleonCoreAPI.Domain;
using System.Threading.Tasks;

namespace ChameleonCoreAPI.API
{
    [ApiController]
    public class ShortUrlController : ControllerBase
    {
        private readonly ShortUrlService _shortUrlService;

        public ShortUrlController(ShortUrlService shortUrlService)
        {
            _shortUrlService = shortUrlService;
        }

        [HttpPost("api/shorten")]
        public async Task<IActionResult> CreateShortUrl([FromBody] ShortUrlRequest request)
        {
            if (!Uri.IsWellFormedUriString(request.OriginalUrl, UriKind.Absolute))
            {
                return BadRequest("Invalid URL.");
            }

            var shortCode = await _shortUrlService.CreateShortUrlAsync(request.OriginalUrl);
            var shortUrl = $"{Request.Scheme}://{Request.Host}/api/shorturl/{shortCode}";

            return Ok(new { ShortUrl = shortUrl });
        }

        [HttpGet("{shortCode}")]
        public async Task<IActionResult> GetOriginalUrl(string shortCode)
        {
            var originalUrl = await _shortUrlService.GetOriginalUrlAsync(shortCode);

            if (originalUrl == null)
            {
                return NotFound("URL encurtada não encontrada.");
            }

            return Redirect(originalUrl);
        }
    }

    public class ShortUrlRequest
    {
        public required string OriginalUrl { get; set; }
    }
}
