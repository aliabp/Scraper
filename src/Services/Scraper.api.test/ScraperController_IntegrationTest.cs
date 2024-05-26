using System.Net;
using System.Text;
using Newtonsoft.Json;
using Scraper.api.Models;

namespace Scraper.api.test;

/*
 * http requests for these tests, handled through ScraperControllerFixture and WebApplicationFactory
 */
public class ScraperController_IntegrationTest : IClassFixture<ScraperControllerFixture>
{
    private readonly HttpClient _client;

    public ScraperController_IntegrationTest(ScraperControllerFixture fixture)
    {
        _client = fixture.CreateClient();
    }

    [Fact]
    public async Task Scraper_ValidObjectPassed_ReturnsOk()
    {
        // Arrange
        var testScraperModel = new ScraperModel
        {
            Keywords = "conveyancing softwares",
            Url = "https://www.smokeball.com.au"
        };

        // make request content
        var content = new StringContent(JsonConvert.SerializeObject(testScraperModel), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/v1/Scraper", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.Equal("1,2,3", responseString); // check response with mock list
    }

    [Fact]
    public async Task Scraper_InvalidKeywords_ReturnsBadRequest()
    {
        // Arrange
        var testScraperModel = new ScraperModel
        {
            Keywords = "", // Invalid Keywords
            Url = "https://www.smokeball.com.au"
        };

        // make request content
        var content = new StringContent(JsonConvert.SerializeObject(testScraperModel), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/v1/Scraper", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}