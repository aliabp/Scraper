namespace Scraper.api.Services;

public interface ISeleniumScraperService : IDisposable
{
    Task<string> GetPageSourceAsync(string url);
}