using Domain.Entities;
using Domain.Services.Exceptions;
using Infrastructure.Repository.Mock;
using NUnit.Framework;

namespace Domain.Services.UnitTests;

public class RegisterProfileServiceTests
{
    private readonly ProfileServiceAggregate _service = new(new MockRepository());

    [SetUp]
    public void Setup()
    {
            
    }

    [Test]
    public void ValidInput()
    {
        var profile = new UserProfile(10,"John", "Smith");
        _service.RegisterUserProfileAsync(profile).GetAwaiter().GetResult();
        Assert.Pass();
    }

    [Test]
    public void ExistingId()
    {
        Assert.Throws<DuplicateUserIdException>(delegate {

            var profile1 = new UserProfile(1,"john", "smith");
            _service.RegisterUserProfileAsync(profile1).GetAwaiter().GetResult();

            var profile2 = new UserProfile(1,"jane", "smith");
            _service.RegisterUserProfileAsync(profile2).GetAwaiter().GetResult();

        });
    }
}