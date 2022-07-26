using MassTransit;
using Shared.Lib.EventBus;

namespace Infrastructure.EventBus.RabbitMQ
{
    public class RabbitEventBus : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public RabbitEventBus(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        async Task IEventBus.publishEventAsync<T>(T evn)
        {
            await _publishEndpoint.Publish<T>(evn);
            
        }
    }
}