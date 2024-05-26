namespace Scraper.api.Services;

/*
 * html parser interface
 * concrete class should implement two functionality:
 * finding a specific tag
 * finding positions of target url in html tags
 */
public interface IHtmlParserService
{
    IEnumerable<string> GetTag(string tagName, string htmlDom);
    Task<IEnumerable<int>> GetUrlPositionsAsync(IEnumerable<string> tags, string url);
}