using Microsoft.Extensions.Configuration;

namespace BookstoreApiTests.Tests.Configuration;

public static class ConfigurationManager
{
    private static IConfiguration? _configuration;
    private static readonly string Environment = System.Environment.GetEnvironmentVariable("TEST_ENVIRONMENT") ?? "Development";

    public static IConfiguration Configuration
    {
        get
        {
            if (_configuration == null)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{Environment}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();

                _configuration = builder.Build();
            }
            return _configuration;
        }
    }

    public static string CurrentEnvironment => Environment;

    public static ApiSettings GetApiSettings()
    {
        var settings = new ApiSettings();
        Configuration.GetSection("ApiSettings").Bind(settings);
        return settings;
    }

    public static LoggingSettings GetLoggingSettings()
    {
        var settings = new LoggingSettings();
        Configuration.GetSection("Logging").Bind(settings);
        return settings;
    }
}
