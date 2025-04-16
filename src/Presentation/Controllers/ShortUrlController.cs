using Microsoft.AspNetCore.Mvc;
using ChameleonCoreAPI.Application;
using System.ComponentModel.DataAnnotations;

namespace ChameleonCoreAPI.Presentation
{
    [ApiController]
    [Produces("application/json")]
    public class ShortUrlController : ControllerBase
    {
        private readonly IShortUrlService _shortUrlService;

        public ShortUrlController(IShortUrlService shortUrlService)
        {
            _shortUrlService = shortUrlService;
        }

        /// <summary>
        /// Redirects to the original URL from a shortened code.
        /// </summary>
        /// <param name="shortCode">The short code to resolve.</param>
        /// <response code="302">Redirects to the original URL</response>
        /// <response code="404">If the short code is not found</response>
        [HttpGet("{shortCode}")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOriginalUrl(string shortCode)
        {
            var originalUrl = await _shortUrlService.GetOriginalUrl(shortCode);

            if (originalUrl == null)
            {
                return NotFound("Shortened URL not found.");
            }

            return Redirect(originalUrl);
        }

        /// <summary>
        /// Creates a shortened URL from the original URL.
        /// </summary>
        /// <param name="request">The request containing the original URL.</param>
        /// <returns>A shortened URL.</returns>
        /// <response code="200">Returns the shortened URL</response>
        /// <response code="400">If the input URL is invalid</response>
        [HttpPost("api/shorten")]
        [ProducesResponseType(typeof(ShortUrlResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateShortUrl([FromBody] ShortUrlRequest request)
        {
            if (!Uri.IsWellFormedUriString(request.OriginalUrl, UriKind.Absolute))
            {
                return BadRequest("Invalid URL. It must be a well-formed absolute URI.");
            }

            var shortCode = await _shortUrlService.CreateShortUrl(request.OriginalUrl);
            var shortUrl = $"{Request.Scheme}://{Request.Host}/{shortCode}";

            return Ok(new ShortUrlResponse { ShortUrl = shortUrl });
        }
    }

    /// <summary>
    /// Request payload for shortening a URL.
    /// </summary>
    public class ShortUrlRequest
    {
        /// <example>https://example.com</example>
        [Required(ErrorMessage = "Original URL is required.")]
        public required string OriginalUrl { get; set; }
    }

    /// <summary>
    /// Response payload containing the shortened URL.
    /// </summary>
    public class ShortUrlResponse
    {
        /// <example>https://scostadavid.dev</example>
        public string ShortUrl { get; set; } = string.Empty;
    }
}
