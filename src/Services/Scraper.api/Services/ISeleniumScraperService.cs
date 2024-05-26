namespace Scraper.api.Services;

public interface ISeleniumScraperService
{
    Task<string> GetPageSourceAsync(string url);
}