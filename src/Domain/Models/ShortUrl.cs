using System.ComponentModel.DataAnnotations;

namespace ChameleonCoreAPI.Domain
{
    public class ShortUrl
    {
        public int Id { get; set; }
        public required string OriginalUrl { get; set; }
        public required string ShortCode { get; set; }
        public int AccessCount { get; set; } = 0;
    }
}
