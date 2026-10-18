namespace BlazorApp1.HttpClient;

public static class HttpClientExtensions
{
    public static void AddApiHttpClient<T>(this IServiceCollection services, string baseUrl, TimeSpan timeout)
        where T : class
    {
        services.AddHttpClient<T>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = timeout;
        });
    }
}