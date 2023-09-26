using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CleanArchitecture.IntegrationTests.TestFixtures;

public static class HttpResponseExtensions
{
    public static async Task<T> ProcessResponse<T>(this HttpResponseMessage response)
    {
        response.EnsureSuccessStatusCode();
        string responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonConvert.DeserializeObject<T>(responseData);
    }

    public static async Task<ProblemDetails> ProcessProblemDetails(this HttpResponseMessage response)
    {
        string responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonConvert.DeserializeObject<ProblemDetails>(responseData);
    }
}