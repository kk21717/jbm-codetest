using Controller.Http.Rest.Models;

namespace Controller.Http.Rest.Util
{
    public interface IHttpClientService
    {
        public Task<TOutput> GetAsync<TOutput>(string url, int timeoutSeconds = 30);
    }
}
