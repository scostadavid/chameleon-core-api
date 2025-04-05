using Xunit;
using Moq;
using ChameleonCoreAPI.Application;
using ChameleonCoreAPI.API;
using Microsoft.AspNetCore.Mvc;

public class ShortUrlControllerTests
{
    [Fact]
    public async Task CreateShortUrl_ShouldReturnShortUrl()
    {
        var mockService = new Mock<IShortUrlService>(MockBehavior.Strict);
        
        mockService.Setup(s => s.CreateShortUrl(It.IsAny<string>()))
                   .ReturnsAsync("abc123");
                
        var controller = new ShortUrlController(mockService.Object);
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
        controller.ControllerContext.HttpContext.Request.Scheme = "https";
        controller.ControllerContext.HttpContext.Request.Host = new HostString("localhost");

        var request = new ShortUrlRequest { OriginalUrl = "https://scostadavid.dev" };

        var result = await controller.CreateShortUrl(request);

        var ok = Assert.IsType<OkObjectResult>(result);
        var value = ok.Value as dynamic;
        string shortUrl = value.ShortUrl;

        Assert.Contains("abc123", shortUrl);
    }
}