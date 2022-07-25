using Domain.Core;
using Domain.Core.Dto;
using MassTransit;

namespace Application.Command
{
    public class RegisterAccountCommand
    {
        readonly IRepository _repository;
        readonly IEventBus _eventBus;
        readonly IPublishEndpoint _publishEvent;

        public RegisterAccountCommand(IRepository repository
            //, IEventBus eventBus
            , IPublishEndpoint publishEvent)
        {
            _repository = repository;
            //_eventBus = eventBus;
            _publishEvent = publishEvent;
        }

        public async Task<RegisterAccountOutput> ExecuteAsync(RegisterAccountInput input)
        {
            var business = new RegisterAccountBusiness(_repository, _eventBus);
            return await business.RegisterAccountAsync(input);
        }
    }
}