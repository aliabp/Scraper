using System.ComponentModel.DataAnnotations;
using Scraper.api.Helper;

namespace Scraper.api.test;

public class IsUrlAttribute_UnitTest
{
    private readonly IsUrlAttribute _attribute = new IsUrlAttribute();

    [Theory]
    [InlineData("http://www.example.com", true)]
    [InlineData("https://example.com", true)]
    [InlineData("http://example.co.uk", true)]
    [InlineData("http://example", false)] // invalid domain
    [InlineData("ftp://example.com", false)] // invalid protocol
    [InlineData("http://example.com/abc?query=1", true)]
    [InlineData("http://example.com/path/", true)]
    [InlineData("http://example.com/path?query", true)]
    [InlineData("example.com", false)] // missing protocol
    [InlineData("", false)] // empty string
    [InlineData(null, false)] // null value
    public void IsValid_ShouldValidateUrlsCorrectly(string url, bool expectedIsValid)
    {
        var result = _attribute.GetValidationResult(url, new ValidationContext(new object()));

        if (expectedIsValid)
        {
            Assert.Equal(ValidationResult.Success, result);
        }
        else
        {
            Assert.NotEqual(ValidationResult.Success, result);
        }
    }
}