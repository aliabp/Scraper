namespace Scraper.api.Services;

/*
 * scraper interface
 * scraper service utilise facade pattern; provides a simplified interface to a complex subsystem
 * concrete class should implement a functionality:
 * scrape and parse a webpage, then find target url in tags
 */
public interface IScraperService
{
    Task<IEnumerable<int>> ScrapeAndParseAsync(string scrapeUrl, string targetUrl);
}