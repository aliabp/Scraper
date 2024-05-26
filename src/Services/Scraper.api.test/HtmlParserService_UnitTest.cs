using Scraper.api.Services;

namespace Scraper.api.test;

public class HtmlParserService_UnitTest
{
    [Fact]
    public void GetTag_TagsFound_ReturnsTags()
    {
        // Arrange
        var tagName = "div"; // Test tag name
        var htmlDom = "<div>content</div><div>another content</div>"; // Test HTML DOM
        
        // Based on htmlDom variable, a list containing these tags expected
        var expectedTags = new List<string> { "<div>content</div>", "<div>another content</div>" };
        HtmlParserService htmlParserService = new HtmlParserService();
        
        // Act
        var result = htmlParserService.GetTag(tagName, htmlDom);

        // Assert
        Assert.Equal(expectedTags, result);
    }
    
    [Fact]
    public async Task GetUrlPositionsAsync_UrlFound_ReturnsPositions()
    {
        // Arrange
        // A list contains 2 test tags
        var tags = new List<string> { "<cite href='https://www.smokeball.com.au'>link</cite>", "<cite href='http://test.com'>another link</cite>" };
        var url = "https://www.smokeball.com.au"; // Test url
        var expectedPositions = new List<int> { 1 }; // Expected position based on test tags
        HtmlParserService htmlParserService = new HtmlParserService();

        // Act
        var result = await htmlParserService.GetUrlPositionsAsync(tags, url);

        // Assert
        Assert.Equal(expectedPositions, result);
    }
}