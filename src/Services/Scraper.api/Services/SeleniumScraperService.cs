using System.Reflection;
using OpenQA.Selenium.Chrome;
using Scraper.api.Helper;

namespace Scraper.api.Services;

public class SeleniumScraperService : ISeleniumScraperService
{
    private readonly ChromeDriver _driver;

    public SeleniumScraperService()
    {
        // set options for web driver
        // use headless option to prevent opening web browser UI
        var options = new ChromeOptions();
        options.AddArgument("--headless");
        options.AddArgument("--disable-gpu");

        try
        {
            // locate Chrome.exe file
            var driverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _driver = new ChromeDriver(driverPath, options);
        }
        catch (Exception e)
        {
            LogHelper.Instance.LogError($"Somthing goes wrong on web driver instantiation: {e.Message}");
        }
    }

    public async Task<string> GetPageSourceAsync(string url)
    {
        // scrape an url and return page source including all dom elements
        return await Task.Run(() =>
        {
            try
            {
                _driver.Navigate().GoToUrl(url);

                // Get the page source as a string
                string pageSource = _driver.PageSource;
                _driver.Quit();

                return pageSource;
            }
            catch (Exception e)
            {
                // an error occured, log the error and return an empty string
                LogHelper.Instance.LogError($"Something went wrong inside GetPageSourceAsync method: {e.Message}");
                return "";
            }

        });
    }
}