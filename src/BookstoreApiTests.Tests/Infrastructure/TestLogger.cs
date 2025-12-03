using Serilog;
using Serilog.Events;
using BookstoreApiTests.Tests.Configuration;

namespace BookstoreApiTests.Tests.Infrastructure;

public static class TestLogger
{
    private static ILogger? _logger;
    private static readonly object Lock = new();

    public static ILogger Log
    {
        get
        {
            if (_logger == null)
            {
                lock (Lock)
                {
                    _logger ??= CreateLogger();
                }
            }
            return _logger;
        }
    }

    private static ILogger CreateLogger()
    {
        var settings = ConfigurationManager.GetLoggingSettings();
        var logLevel = Enum.Parse<LogEventLevel>(settings.LogLevel);

        var config = new LoggerConfiguration()
            .MinimumLevel.Is(logLevel)
            .Enrich.WithProperty("Environment", ConfigurationManager.CurrentEnvironment)
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}");

        if (settings.LogToFile)
        {
            config.WriteTo.File(
                settings.LogPath,
                rollingInterval: RollingInterval.Day,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}");
        }

        return config.CreateLogger();
    }

    public static void Info(string message) => Log.Information(message);
    public static void Debug(string message) => Log.Debug(message);
    public static void Warning(string message) => Log.Warning(message);
    public static void Error(string message, Exception? ex = null) => Log.Error(ex, message);
}
