using Domain.Entities;
using Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.SQLServer;

public class SQLServerRepository : IRepository
{
    private readonly DbContextOptions<SQLServerDBContext> _options;

    public SQLServerRepository(DbContextOptions<SQLServerDBContext> options)
    {
        _options = options;
    }

    async Task<bool> IRepository.UserIdExistsAsync(int userId)
    {
        await using var context = new SQLServerDBContext(_options);
        return await context.UserProfiles.AnyAsync(a => a.UserId == userId);
    }

    async Task IRepository.InsertUserProfileAsync(UserProfile userProfile)
    {
        await using var context = new SQLServerDBContext(_options);
        var dbEntity = new Entities.UserProfile
        {
            UserId = userProfile.UserId,
            FirstName = userProfile.FirstName,
            LastName = userProfile.LastName
        };

        context.UserProfiles.Add(dbEntity);
        await context.SaveChangesAsync();
    }

    public async Task<UserProfile> GetUserProfileAsync(int userId)
    {
        await using var context = new SQLServerDBContext(_options);
        var entity = await context.UserProfiles.SingleOrDefaultAsync(a => a.UserId == userId);
        if (entity == null)
            throw new Exception("Entity not found");
        return new UserProfile(entity.UserId,entity.FirstName,entity.LastName);
    }

}