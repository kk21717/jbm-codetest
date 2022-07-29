using Controller.Http.Rest.Exceptions;
using Controller.Http.Rest.Models;
using Controller.Http.Rest.Util;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Controller.Http.Rest.Aggregators
{
    public class UserAggregator : IUserAggregator
    {
        private readonly IOptions<ExternalResourcesOptions> _options;
        private readonly IHttpClientService _httpClientService;

        public UserAggregator(
             IOptions<ExternalResourcesOptions> options,
             IHttpClientService httpClientService)
        {
            _options = options;
            _httpClientService = httpClientService;
        }
        public async Task<UserView> GetUserViewAsync(int userId)
        {
            try
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
            catch (Exception ex)
            {
                throw new DownstreamUnavailableException();
            }
        }

        private async Task<UserAccount> callGetUserAccountAsync(int userId)
        {
            var url = _options.Value.UserAccounts_ResourceUrl.Replace("{userId}", userId.ToString());
            var userAccount = await _httpClientService.GetAsync<UserAccount>(url);
            return userAccount;
        }

        private async Task<UserProfile> callGetUserProfileAsync(int userId)
        {
            var url = _options.Value.UserProfiles_ResourceUrl.Replace("{userId}", userId.ToString());
            var userProfile = await _httpClientService.GetAsync<UserProfile>(url);
            return userProfile;
        }

    }
}
