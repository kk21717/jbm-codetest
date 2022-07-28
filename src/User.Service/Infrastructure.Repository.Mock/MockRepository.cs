using Domain.Entities;
using Domain.Services;
using Domain.Services.Exceptions;

namespace Infrastructure.Repository.Mock;

public class MockRepository : IRepository
{
    //implementing a fake dummy repository
    private readonly List<UserProfile> _profiles = new();

    Task<bool> IRepository.UserIdExistsAsync(int userId)
    {
        return Task.FromResult(_profiles.Any(a => a.UserId == userId));
    }

    Task IRepository.InsertUserProfileAsync(UserProfile newProfile)
    {
        _profiles.Add(newProfile);
        return Task.CompletedTask;
    }

    public Task<UserProfile> GetUserProfileAsync(int userId)
    {
        var res = _profiles.SingleOrDefault(x => x.UserId == userId);
        if (res == null)
            throw new UserIdNotFoundException();
        return Task.FromResult(res);
    }
}