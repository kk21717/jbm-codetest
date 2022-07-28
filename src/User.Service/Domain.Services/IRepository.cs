using Domain.Entities;

namespace Domain.Services;

public interface IRepository
{
    public Task InsertUserProfileAsync(UserProfile userProfile);

    public Task<UserProfile> GetUserProfileAsync(int userId);

    public Task<bool> UserIdExistsAsync(int userId);
}