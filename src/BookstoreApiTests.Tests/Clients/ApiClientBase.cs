using System.Diagnostics;
using RestSharp;
using BookstoreApiTests.Tests.Configuration;
using BookstoreApiTests.Tests.Infrastructure;

namespace BookstoreApiTests.Tests.Clients;

public abstract class ApiClientBase : IDisposable
{
    protected readonly RestClient Client;
    protected readonly ApiSettings Settings;
    private bool _disposed;

    protected ApiClientBase()
    {
        Settings = ConfigurationManager.GetApiSettings();

        var options = new RestClientOptions(Settings.BaseUrl)
        {
            Timeout = TimeSpan.FromSeconds(Settings.TimeoutSeconds)
        };

        Client = new RestClient(options);
        TestLogger.Info($"API Client initialized - BaseUrl: {Settings.BaseUrl}, Environment: {ConfigurationManager.CurrentEnvironment}");
    }

    protected async Task<RestResponse> ExecuteWithRetry(RestRequest request)
    {
        var sw = Stopwatch.StartNew();

        TestLogger.Debug($"Request: {request.Method} {request.Resource}");

        var response = await RetryPolicy.GetRetryPolicy()
            .ExecuteAsync(() => Client.ExecuteAsync(request));

        sw.Stop();
        LogResponse(response, sw.ElapsedMilliseconds);

        return response;
    }

    protected async Task<RestResponse<T>> ExecuteWithRetry<T>(RestRequest request)
    {
        var sw = Stopwatch.StartNew();

        TestLogger.Debug($"Request: {request.Method} {request.Resource}");

        var response = await RetryPolicy.GetRetryPolicy<T>()
            .ExecuteAsync(() => Client.ExecuteAsync<T>(request));

        sw.Stop();
        LogResponse(response, sw.ElapsedMilliseconds);

        return response;
    }

    private void LogResponse(RestResponse response, long elapsedMs)
    {
        var logMessage = $"Response: {(int)response.StatusCode} {response.StatusCode} - {elapsedMs}ms";

        if (response.IsSuccessful)
            TestLogger.Debug(logMessage);
        else
            TestLogger.Warning($"{logMessage} - Error: {response.ErrorMessage}");
    }

    protected static void AssertResponseTime(long elapsedMs, int maxMs)
    {
        if (elapsedMs > maxMs)
        {
            TestLogger.Warning($"Response time {elapsedMs}ms exceeded threshold {maxMs}ms");
        }
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
                Client.Dispose();
            }
            _disposed = true;
        }
    }
}
