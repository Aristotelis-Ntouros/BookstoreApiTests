namespace BookstoreApiTests.Tests.Configuration;

public class ApiSettings
{
    public string BaseUrl { get; set; } = "https://fakerestapi.azurewebsites.net";
    public int TimeoutSeconds { get; set; } = 30;
    public int MaxRetryAttempts { get; set; } = 3;
    public int RetryDelayMs { get; set; } = 1000;
    public int MaxResponseTimeMs { get; set; } = 5000;
}

public class LoggingSettings
{
    public string LogLevel { get; set; } = "Information";
    public bool LogToFile { get; set; } = true;
    public string LogPath { get; set; } = "logs/test-{Date}.log";
}
