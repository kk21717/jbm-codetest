using Application.Command.Mapping;
using Application.Command.RegisterUser;
using MassTransit;
using MediatR;
using Shared.Lib.EventBus.AuthService;

namespace Controller.Worker.EventListeners.AuthService;

public class AccountRegisteredEventListener : IConsumer<AccountRegisteredEvent>
{
    private readonly ISender _mediatorSender;

    public AccountRegisteredEventListener(ISender mediatorSender)
    {
        _mediatorSender = mediatorSender;
    }

    public async Task Consume(ConsumeContext<AccountRegisteredEvent> context)
    {
        var profileCommandInput = AutoMapping.Mapper.Map<RegProfileCommandInput>(context.Message);
        await _mediatorSender.Send(new RegProfileCommand(profileCommandInput));
        //use context root fields like context.SentTime to get metadata about event
    }


}