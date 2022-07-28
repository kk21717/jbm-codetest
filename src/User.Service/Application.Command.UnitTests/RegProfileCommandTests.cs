using Application.Command.RegisterUser;
using Infrastructure.Repository.Mock;
using NUnit.Framework;
using System.Threading;

namespace Application.Command.UnitTests;

public class RegProfileCommandTests
{
    private readonly RegProfileCommandHandler _commandHandler = new(new MockRepository());

    [SetUp]
    public void Setup()
    {
            
    }

    [Test]
    public void ValidCommandInput()
    {
        var command = new RegProfileCommand(new RegProfileCommandInput
        {
            UserId = 100,
            Phone = "+989121234567",
            Email = "valid@email.com",
            FirstName = "Kamran",
            LastName = "Karami"
        });

        var cancellationToken = new CancellationToken();
        _commandHandler.Handle(command, cancellationToken).GetAwaiter().GetResult();

        Assert.Pass();
    }
}