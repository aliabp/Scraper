using Serilog;
using ILogger = Serilog.ILogger;

namespace Scraper.api.Helper;

public sealed class LogHelper : ILogHelper
{
    // make instance of class in first call and thread safe using Lazy<T> class
    private static readonly Lazy<LogHelper> Lazy =
        new(() => new LogHelper());

    // get instance of class, this instance created just once and remain till end of application
    public static LogHelper Instance => Lazy.Value;

    // create instance of Serilog
    private readonly ILogger _logger;

    private LogHelper()
    {
        // config Serilog including path, filename and log message format
        _logger = new LoggerConfiguration()
            .WriteTo.File(
                Path.Combine(Directory.GetCurrentDirectory() + "/Logs/log-.txt"),
                rollingInterval: RollingInterval.Day, 
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
            .CreateLogger();
    }

    public void LogInformation(string message)
    {
        // log information message
        _logger.Information(message);
    }

    public void LogError(string message)
    {
        // log error message
        _logger.Error(message);
    }
    
    public void CloseAndFlush()
    {
        // close log object at the end of application
        Log.CloseAndFlush();
    }
}