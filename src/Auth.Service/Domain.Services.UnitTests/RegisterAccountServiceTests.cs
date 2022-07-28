using Domain.Entities;
using Domain.Services.Exceptions;
using Infrastructure.Repository.Mock;
using NUnit.Framework;

namespace Domain.Services.UnitTests;

public class RegisterAccountServiceTests
{
    private readonly AccountServiceAggregate _service = new(new MockRepository());

    [SetUp]
    public void Setup()
    {
            
    }

    [Test]
    public void ValidInput()
    {
        var account = new Account("+989126164570", "kamran@gmail.com");
        var newId = _service.RegisterAccountAsync(account).GetAwaiter().GetResult();
        Assert.Positive(newId);
    }

    [Test]
    public void InvalidPhone()
    {
        Assert.Throws<InvalidPhoneException>(delegate {
            var account = new Account("some invalid phone", "valid@email.com");
            _service.RegisterAccountAsync(account).GetAwaiter().GetResult();
        });
    }

    [Test]
    public void InvalidEmail()
    {
        Assert.Throws<InvalidEmailException>(delegate {
            var account = new Account("+989126453214", "some invalid email");
            _service.RegisterAccountAsync(account).GetAwaiter().GetResult();
        });
    }

    [Test]
    public void DuplicatePhone()
    {
        Assert.Throws<DuplicatePhoneException>(delegate {

            var account1 = new Account("+989126453214", "mail1@valid.com");
            _service.RegisterAccountAsync(account1).GetAwaiter().GetResult();

            var account2 = new Account("+989126453214", "another@valid.com");
            _service.RegisterAccountAsync(account2).GetAwaiter().GetResult();

        });
    }
}