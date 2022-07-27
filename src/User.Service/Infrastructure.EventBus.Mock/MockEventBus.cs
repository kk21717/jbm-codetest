using Shared.Lib.EventBus;

namespace Infrastructure.EventBus.Mock;

public class MockEventBus : IEventBus
{
    async Task IEventBus.publishEventAsync<T>(T evn)
    {
        await Task.CompletedTask;
    }
}