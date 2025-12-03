using Microsoft.Extensions.Configuration;

namespace BookstoreApiTests.Tests.Configuration;

public static class ConfigurationManager
{
    private static IConfiguration? _configuration;

    public static IConfiguration Configuration
    {
        get
        {
            if (_configuration == null)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();

                _configuration = builder.Build();
            }
            return _configuration;
        }
    }

    public static ApiSettings GetApiSettings()
    {
        var settings = new ApiSettings();
        Configuration.GetSection("ApiSettings").Bind(settings);
        return settings;
    }
}
