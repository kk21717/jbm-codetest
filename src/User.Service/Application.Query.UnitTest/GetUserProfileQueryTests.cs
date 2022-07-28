using Application.Query.GetUserProfile;
using Domain.Entities;
using Domain.Services;
using Domain.Services.Exceptions;
using Infrastructure.Repository.Mock;
using NUnit.Framework;
using System.Threading;

namespace Application.Query.UnitTest;

public class GetUserProfileQueryTests
{
    private IRepository _mockRepository;
    private GetUserProfileQueryHandler _queryHandler;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new MockRepository();
        _queryHandler = new(_mockRepository);
    }

    [Test]
    public void ReturnsProfileForExistingUserId()
    {
        var profileMartin = new UserProfile()
        {
            UserId = 1,
            FirstName = "Martin",
            LastName = "Fowler"
        };
        var profileGreg = new UserProfile()
        {
            UserId = 2,
            FirstName = "Greg",
            LastName = "Young"
        };

        _mockRepository.InsertUserProfileAsync(profileMartin);
        _mockRepository.InsertUserProfileAsync(profileGreg);

        var query = new GetUserProfileQuery(profileGreg.UserId);
        var cancellationToken = new CancellationToken();
        var fetchedProfile = _queryHandler.Handle(query, cancellationToken).GetAwaiter().GetResult();

        Assert.AreEqual(fetchedProfile.UserId, profileGreg.UserId);
        Assert.AreEqual(fetchedProfile.FirstName, profileGreg.FirstName);
        Assert.AreEqual(fetchedProfile.LastName, profileGreg.LastName);
    }

    [Test]
    public void ThrowExceptionForNotExistingUserId()
    {
        Assert.Throws<UserIdNotFoundException>(delegate {
            var query = new GetUserProfileQuery(65465); //some non-existing Userid
            var cancellationToken = new CancellationToken();
            var fetchedProfile = _queryHandler.Handle(query, cancellationToken).GetAwaiter().GetResult();
        });
    }
}
