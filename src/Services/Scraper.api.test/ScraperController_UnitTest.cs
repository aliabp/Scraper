using Microsoft.AspNetCore.Mvc;
using Scraper.api.Controllers;
using Scraper.api.Models;
using Scraper.api.Services;

namespace Scraper.api.test;

public class ScraperController_UnitTest
{
    [Fact]
    public async void Scraper_ValidObjectPassed_ReturnsOk()
    {
        // Arrange
        var testScraperModel = new ScraperModel()
        {
            Url = "https://www.smokeball.com.au",
            Keywords = "Conveyancing Softwares"
        };
        string searchUrl = $"https://www.google.com.au/search?num=100&q={Uri.EscapeDataString(testScraperModel.Keywords)}";
        
        // create a mock for IScraperService
        Mock<IScraperService> mockService = new Mock<IScraperService>();
        mockService.Setup(service => 
            service.ScrapeAndParseAsync(searchUrl, testScraperModel.Url))
            .Returns(GetMockPositions());
        ScraperController controller = new ScraperController(mockService.Object);

        // Act
        var response = await controller.Scraper(testScraperModel);

        // Assert
        Assert.IsType<OkObjectResult>(response);
    }
    
    [Fact]
    public async void Scraper_InvalidUrl_ReturnsBadRequest()
    {
        // Arrange
        var testScraperModel = new ScraperModel()
        {
            Url = "smokeball",
            Keywords = "Conveyancing Softwares"
        };
        string searchUrl = $"https://www.google.com.au/search?num=100&q={Uri.EscapeDataString(testScraperModel.Keywords)}";
        
        // create a mock for IScraperService
        Mock<IScraperService> mockService = new Mock<IScraperService>();
        mockService.Setup(service => 
                service.ScrapeAndParseAsync(searchUrl, testScraperModel.Url))
            .Returns(GetMockPositions());
        ScraperController controller = new ScraperController(mockService.Object);
        
        // add a test error to controller modelstate
        controller.ModelState.AddModelError("Url", "The Url field must be a valid URI.");

        // Act
        var response = await controller.Scraper(testScraperModel);

        // Assert
        Assert.IsType<BadRequestObjectResult>(response);
    }
    
    [Fact]
    public async void Scraper_InvalidKeywordsLenght_ReturnsBadRequest()
    {
        // Arrange
        var testScraperModel = new ScraperModel()
        {
            Url = "https://www.smokeball.com.au",
            Keywords = "C"
        };
        string searchUrl = $"https://www.google.com.au/search?num=100&q={Uri.EscapeDataString(testScraperModel.Keywords)}";
        
        // create a mock for IScraperService
        Mock<IScraperService> mockService = new Mock<IScraperService>();
        mockService.Setup(service => 
                service.ScrapeAndParseAsync(searchUrl, testScraperModel.Url))
            .Returns(GetMockPositions());
        ScraperController controller = new ScraperController(mockService.Object);
        
        // add a test error to controller modelstate
        controller.ModelState.AddModelError("Keywords", "Keywords length is out of (3-100)");

        // Act
        var response = await controller.Scraper(testScraperModel);

        // Assert
        Assert.IsType<BadRequestObjectResult>(response);
    }

    private Task<IEnumerable<int>> GetMockPositions()
    {
        // return mock positions which target url founded
        var positions = new List<int>()
        {
            1,
            2,
            3,
            10
        };
        return Task.FromResult<IEnumerable<int>>(positions);
    }
}