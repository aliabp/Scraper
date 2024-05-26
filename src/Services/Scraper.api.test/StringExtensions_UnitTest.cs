using Scraper.api.Helper;

namespace Scraper.api.test;

public class StringExtensions_UnitTest
{
    [Fact]
    public void GetHtmlTags_ReturnsCorrectTags()
    {
        // Arrange
        string html = "<div>Hello</div><div>World</div>";
        string tagName = "div";

        // Act
        var result = StringExtensions.GetHtmlTags(html, tagName);

        // Assert
        // check list contains target tags
        Assert.Collection(result,
            item => Assert.Equal("<div>Hello</div>", item),
            item => Assert.Equal("<div>World</div>", item));
    }

    [Fact]
    public void GetHtmlTags_ReturnsEmpty()
    {
        // Arrange
        string html = "<span>Hello</span><span>World</span>";
        string tagName = "div";

        // Act
        var result = StringExtensions.GetHtmlTags(html, tagName);

        // Assert
        Assert.Empty(result);
    }
}