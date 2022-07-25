using Domain.Core;
using Domain.Core.Event;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventBus.RabbitMQ
{
    public class RabbitEventBus : IEventBus
    {
        readonly IOptions<RabbitEventBusOptions> _options;
        public RabbitEventBus(IOptions<RabbitEventBusOptions> options)
        {
            _options = options;
        }
        Task IEventBus.pushAccountRegisteredEvent(AccountRegisteredEvent e)
        {
            throw new NotImplementedException();
        }
    }
}