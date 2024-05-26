using Microsoft.Extensions.Options;
using Scraper.api.Models;
using Scraper.api.Services;

namespace Scraper.api.test;

public class ScraperService_UnitTest
{

    // inject mocks for external services
    
    private readonly Mock<IHtmlParserService> _mockHtmlParserService;
    private readonly Mock<ISeleniumScraperService> _mockSeleniumScraperService;
    private readonly Mock<IOptionsSnapshot<ApplicationOptions>> _mockOptionsSnapshot;
    private readonly ApplicationOptions _applicationOptions;

    public ScraperService_UnitTest()
    {
        _mockHtmlParserService = new Mock<IHtmlParserService>();
        _mockSeleniumScraperService = new Mock<ISeleniumScraperService>();
        _mockOptionsSnapshot = new Mock<IOptionsSnapshot<ApplicationOptions>>();
        _applicationOptions = new ApplicationOptions
        {
            UrlHolderTagName = "cite"
        };
        _mockOptionsSnapshot.Setup(m => m.Value).Returns(_applicationOptions);
    }

    [Fact]
    public async Task ScrapeAndParseAsync_UrlFound_ReturnsPositions()
    {
        // Arrange
        var scrapeUrl = "https://google.com.au";
        var targetUrl = "https://www.smokeball.com.au";
        var htmlContent = "<html><body><div class='g'><cite>https://www.smokeball.com.au</cite></div></body></html>";
        var tags = new List<string> { "<cite href='https://www.smokeball.com.au'></cite>" };
        var positions = new List<int> { 1 };

        // mock GetTag in HtmlParserService
        _mockHtmlParserService
            .Setup(service => 
                service.GetTag(_applicationOptions.UrlHolderTagName, htmlContent))
                .Returns(tags);

        // mock GetUrlPositionsAsync in HtmlParserService
        _mockHtmlParserService
            .Setup(service => 
                service.GetUrlPositionsAsync(tags, targetUrl))
                .ReturnsAsync(positions);

        var scraperService = new ScraperService(_mockHtmlParserService.Object, _mockOptionsSnapshot.Object, _mockSeleniumScraperService.Object);

        // mock SeleniumScraperService to return the expected HTML content
        _mockSeleniumScraperService
            .Setup(service => 
                service.GetPageSourceAsync(scrapeUrl))
                .ReturnsAsync(htmlContent);

        // Act
        var result = await scraperService.ScrapeAndParseAsync(scrapeUrl, targetUrl);

        // Assert
        Assert.Equal(positions, result);
    }

    [Fact]
    public async Task ScrapeAndParseAsync_HandlesException_ReturnsEmptyList()
    {
        // Arrange
        var scrapeUrl = "https://google.com.au";
        var targetUrl = "https://www.smokeball.com.au";

        // mock GetTag in HtmlParserService with an exception
        _mockHtmlParserService
            .Setup(service => service.GetTag(It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception("Test exception"));

        var scraperService = new ScraperService(_mockHtmlParserService.Object, _mockOptionsSnapshot.Object, _mockSeleniumScraperService.Object);

        // Act
        var result = await scraperService.ScrapeAndParseAsync(scrapeUrl, targetUrl);

        // Assert
        Assert.Empty(result);
    }
}