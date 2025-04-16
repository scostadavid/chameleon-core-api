using System.Threading.Tasks;
using Xunit;
using Moq;
using ChameleonCoreAPI.Domain;
using ChameleonCoreAPI.Application;

public class ShortUrlServiceTests
{
    [Fact]
    public async Task CreateShortUrl_ShouldReturnShortCode()
    {
        var mockRepo = new Mock<IShortUrlRepository>(MockBehavior.Strict);
        
        mockRepo.Setup(r => r.Create(It.IsAny<ShortUrl>()))
            .Returns(Task.CompletedTask);

        var service = new ShortUrlService(mockRepo.Object);

        var originalUrl = "https://scostadavid.dev";

        var shortCode = await service.CreateShortUrl(originalUrl);

        Assert.False(string.IsNullOrWhiteSpace(shortCode));
        Assert.Equal(6, shortCode.Length);
    }

    [Fact]
    public async Task CreateShortUrl_ShouldGenerateDifferentCodesForSameUrl()
    {
        var mockRepo = new Mock<IShortUrlRepository>(MockBehavior.Strict);

        mockRepo.Setup(r => r.Create(It.IsAny<ShortUrl>()))
            .Returns(Task.CompletedTask);
        
        var service = new ShortUrlService(mockRepo.Object);
        var url = "https://scostadavid.dev";

        var code1 = await service.CreateShortUrl(url);
        var code2 = await service.CreateShortUrl(url);

        Assert.NotEqual(code1, code2);
    }
}