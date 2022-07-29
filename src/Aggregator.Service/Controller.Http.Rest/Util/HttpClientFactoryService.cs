using System.Text.Json;
using Controller.Http.Rest.Models;

namespace Controller.Http.Rest.Util
{
    public class HttpClientFactoryService : IHttpClientServiceImplementation
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _options;

        public HttpClientFactoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task Execute()
        {
            await GetCompaniesWithHttpClientFactory();
        }

        private async Task GetCompaniesWithHttpClientFactory()
        {
            var httpClient = _httpClientFactory.CreateClient("UserProfilesClient");
            using (var response = await httpClient.GetAsync("", HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                var companies = await JsonSerializer.DeserializeAsync<UserProfile>(stream, _options);
            }
        }
    }
}
