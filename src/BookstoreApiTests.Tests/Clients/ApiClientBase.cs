using System.Net.Http.Json;
using System.Text.Json;
using BookstoreApiTests.Tests.Configuration;

namespace BookstoreApiTests.Tests.Clients;

public abstract class ApiClientBase : IDisposable
{
    protected readonly HttpClient HttpClient;
    protected readonly string BaseUrl;
    private bool _disposed;

    protected static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    protected ApiClientBase()
    {
        var settings = ConfigurationManager.GetApiSettings();
        BaseUrl = settings.BaseUrl;

        HttpClient = new HttpClient
        {
            BaseAddress = new Uri(BaseUrl),
            Timeout = TimeSpan.FromSeconds(settings.TimeoutSeconds)
        };
        HttpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    protected async Task<HttpResponseMessage> GetAsync(string endpoint)
    {
        return await HttpClient.GetAsync(endpoint);
    }

    protected async Task<T?> GetAsync<T>(string endpoint)
    {
        var response = await HttpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>(JsonOptions);
    }

    protected async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data)
    {
        return await HttpClient.PostAsJsonAsync(endpoint, data, JsonOptions);
    }

    protected async Task<HttpResponseMessage> PutAsync<T>(string endpoint, T data)
    {
        return await HttpClient.PutAsJsonAsync(endpoint, data, JsonOptions);
    }

    protected async Task<HttpResponseMessage> DeleteAsync(string endpoint)
    {
        return await HttpClient.DeleteAsync(endpoint);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                HttpClient.Dispose();
            }
            _disposed = true;
        }
    }
}
