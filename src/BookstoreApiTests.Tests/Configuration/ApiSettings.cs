namespace BookstoreApiTests.Tests.Configuration;

public class ApiSettings
{
    public string BaseUrl { get; set; } = "https://fakerestapi.azurewebsites.net";
    public int TimeoutSeconds { get; set; } = 30;
}
