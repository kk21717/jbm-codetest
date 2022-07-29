using Controller.Http.Rest.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Controller.Http.Rest.Aggregators
{
    public class UserAggregator : IUserAggregator
    {
        private readonly IOptions<ExternalResourcesOptions> _options;
        private readonly IHttpClientFactory _httpClientFactory;

        public UserAggregator(
             IOptions<ExternalResourcesOptions> options,
             IHttpClientFactory httpClientFactory)
        {
           _options = options;
           _httpClientFactory = httpClientFactory;
        }
        public async Task<UserView> GetUserViewAsync(int userId)
        {
            var userView = new UserView();
            var calls = new List<Task>();

            var userAccountTask = callGetUserAccountAsync(userId);
            var userProfileTask = callGetUserProfileAsync(userId);

            calls.Add(userAccountTask);
            calls.Add(userProfileTask);

            Task.WaitAll(calls.ToArray());

            userView.UserId = userAccountTask.Result.UserId;
            userView.Phone = userAccountTask.Result.Phone;
            userView.Email = userAccountTask.Result.Email;

            userView.FirstName = userProfileTask.Result.FirstName;
            userView.LastName = userProfileTask.Result.LastName;

            return userView;
        }

        private async Task<UserAccount> callGetUserAccountAsync(int userId)
        {
            var url = _options.Value.UserAccounts_ResourceUrl.Replace("{userId}", userId.ToString());
            var userAccount = await SendGetRequestAsync<UserAccount>(url);
            return userAccount;
        }

        private async Task<UserProfile> callGetUserProfileAsync(int userId)
        {
            var url = _options.Value.UserProfiles_ResourceUrl.Replace("{userId}", userId.ToString());
            var userProfile = await SendGetRequestAsync<UserProfile>(url);
            return userProfile;
        }

        private async Task<TOutput> SendGetRequestAsync<TOutput>(string resourceUrl)
        {
            TOutput? responseObject = default(TOutput);
            try
            {
                using var client = _httpClientFactory.CreateClient("aggregator-client-get");
                var response = await client.GetAsync(resourceUrl);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                responseObject = JsonSerializer.Deserialize<TOutput>(responseBody);
                return responseObject;
            }
            catch (Exception exception)
            {
                //log exception ...
                throw;
            }
        }

        ////implementation for sending Post request, no need at the moment
        //private async Task<TOutput> SendPostRequestAsync<TInput, TOutput>(TInput input, string resourceUrl)
        //{
        //    TOutput responseObject;
        //    try
        //    {
        //        var jsonData = JsonSerializer.Serialize(input);

        //        using var client = _httpClientFactory.CreateClient("aggregator-client-post");
        //        var requestContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        //        var response = await client.PostAsync(resourceUrl, requestContent);
        //        response.EnsureSuccessStatusCode();
        //        var responseBody = await response.Content.ReadAsStringAsync();
        //        responseObject = JsonSerializer.Deserialize<TOutput>(responseBody);
        //        return responseObject;
        //    }
        //    catch (Exception exception)
        //    {
        //        //log exception ...
        //        throw;
        //    }
        //}
    }
}
