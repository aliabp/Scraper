using Microsoft.Extensions.Options;
using Scraper.api.Helper;
using Scraper.api.Models;

namespace Scraper.api.Services;

public class ScraperService : IScraperService
{
    private readonly IHtmlParserService _htmlParserService;
    private readonly ApplicationOptions _applicationOptions;
    private readonly ISeleniumScraperService _seleniumScraperService;

    public ScraperService(IHtmlParserService htmlParserService, 
        IOptionsSnapshot<ApplicationOptions> applicationOptions, 
        ISeleniumScraperService seleniumScraperService)
    {
        _htmlParserService = htmlParserService;
        _applicationOptions = applicationOptions.Value;
        _seleniumScraperService = seleniumScraperService;
    }

    public async Task<IEnumerable<int>> ScrapeAndParseAsync(string scrapeUrl, string targetUrl)
    {
        try
        {
            string html;
        
            // get page source of scrapeUrl utilising Selenium web driver
            html = await _seleniumScraperService.GetPageSourceAsync(scrapeUrl);
        
            // get a list of specific tag in page source
            var cites = _htmlParserService.GetTag(_applicationOptions.UrlHolderTagName, html);
        
            // get a list of positions where target url is found
            var positions = await _htmlParserService.GetUrlPositionsAsync(cites, targetUrl);
        
            return positions;
        }
        catch (Exception e)
        {
            // an error occured, log the error and return an empty array of positions
            LogHelper.Instance.LogError($"Something went wrong inside ScrapeAndParseAsync method: {e.Message}");
            return new List<int>();
        }
    }
}