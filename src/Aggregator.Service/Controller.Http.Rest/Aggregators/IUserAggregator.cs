using Controller.Http.Rest.Models;

namespace Controller.Http.Rest.Aggregators
{
    public interface IUserAggregator
    {
        public Task<UserView> GetUserViewAsync(int userId);
    }
}
