namespace Shared.Lib.EventBus
{
    public interface IEventBus
    {
        public Task publishEventAsync<T>(T evn) where T : BaseEvent;
    }
}