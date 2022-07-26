using Application.Command.RegisterUser;
using Infrastructure.EventBus.Mock;
using Infrastructure.Repository.Mock;
using NUnit.Framework;
using System.Threading;

namespace Application.Command.UnitTests
{
    public class RegUserCommandTests
    {
        private RegUserCommandHandler _commandHandler;

        [SetUp]
        public void Setup()
        {
            _commandHandler  = new RegUserCommandHandler(new MockRepository(), new MockEventBus());
        }

        [Test]
        public void Test1()
        {
            var command = new RegUserCommand(new RegUserCommandInput()
            {
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
}