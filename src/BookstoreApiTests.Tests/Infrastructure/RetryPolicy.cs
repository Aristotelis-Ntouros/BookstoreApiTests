using Polly;
using Polly.Retry;
using RestSharp;
using BookstoreApiTests.Tests.Configuration;

namespace BookstoreApiTests.Tests.Infrastructure;

public static class RetryPolicy
{
    private static readonly ApiSettings Settings = ConfigurationManager.GetApiSettings();

    public static AsyncRetryPolicy<RestResponse> GetRetryPolicy()
    {
        return Policy<RestResponse>
            .Handle<HttpRequestException>()
            .OrResult(r => (int)r.StatusCode >= 500)
            .WaitAndRetryAsync(
                Settings.MaxRetryAttempts,
                retryAttempt => TimeSpan.FromMilliseconds(Settings.RetryDelayMs * retryAttempt),
                onRetry: (outcome, timespan, retryCount, context) =>
                {
                    TestLogger.Warning($"Retry {retryCount} after {timespan.TotalMilliseconds}ms - Status: {outcome.Result?.StatusCode}");
                });
    }

    public static AsyncRetryPolicy<RestResponse<T>> GetRetryPolicy<T>()
    {
        return Policy<RestResponse<T>>
            .Handle<HttpRequestException>()
            .OrResult(r => (int)r.StatusCode >= 500)
            .WaitAndRetryAsync(
                Settings.MaxRetryAttempts,
                retryAttempt => TimeSpan.FromMilliseconds(Settings.RetryDelayMs * retryAttempt),
                onRetry: (outcome, timespan, retryCount, context) =>
                {
                    TestLogger.Warning($"Retry {retryCount} after {timespan.TotalMilliseconds}ms - Status: {outcome.Result?.StatusCode}");
                });
    }
}
