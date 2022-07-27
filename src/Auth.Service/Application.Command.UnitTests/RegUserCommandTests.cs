using Application.Command.RegisterUser;
using Infrastructure.EventBus.Mock;
using Infrastructure.Repository.Mock;
using NUnit.Framework;
using System.Threading;

namespace Application.Command.UnitTests;

public class RegUserCommandTests
{
    private readonly RegUserCommandHandler _commandHandler = new(new MockRepository(), new MockEventBus());

    [SetUp]
    public void Setup()
    {
            
    }

    [Test]
    public void ValidCommandInput()
    {
        var command = new RegUserCommand(new RegUserCommandInput
        {
            Phone = "+989121234567",
            Email = "valid@email.com",
            FirstName = "Kamran",
            LastName = "Karami"
        });

        var cancellationToken = new CancellationToken();
        var res = _commandHandler.Handle(command, cancellationToken).GetAwaiter().GetResult();

        Assert.Pass();
    }
}