using System.Text.RegularExpressions;

namespace Scraper.api.Helper;

public static class StringExtensions
{
    public static IEnumerable<string> GetHtmlTags(this string html, string tagName)
    {
        var regex = new Regex($"<{tagName}.*?>(.*?)</{tagName}>", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
        var matches = regex.Matches(html);

        foreach (Match match in matches)
        {
            yield return match.Value;
        }
    }
}