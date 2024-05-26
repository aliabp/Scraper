namespace Scraper.api.Helper;

/*
 * logger interface
 * concrete class should implement these functionality:
 * log information
 * log errors
 * close log object
 */
public interface ILogHelper
{
    void LogInformation(string message);
    void LogError(string message);
    void CloseAndFlush();
}