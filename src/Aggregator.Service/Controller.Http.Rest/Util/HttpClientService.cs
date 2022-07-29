using Controller.Http.Rest.Models;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Controller.Http.Rest.Util;

public class HttpClientService : IHttpClientService
{

    public async Task<TOutput> GetAsync<TOutput>(string url,int timeoutSeconds = 30)
    {
        HttpClient _httpClient = new();
        //_httpClient.DefaultRequestHeaders.Clear();
        //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _httpClient.BaseAddress = new Uri(url);
        _httpClient.Timeout = new TimeSpan(0, 0, timeoutSeconds);
        var response = await _httpClient.GetAsync("");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var res = JsonSerializer.Deserialize<TOutput>(content, _options);
        return res;

    }
}
