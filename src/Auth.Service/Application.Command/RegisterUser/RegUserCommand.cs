
using Application.Command.Mapping;
using Domain.Entities;
using Domain.Services;
using MediatR;
using Shared.Lib.EventBus;
using Shared.Lib.EventBus.AuthService;

namespace Application.Command.RegisterUser;

public class RegUserCommand : BaseCommand<RegUserCommandOutput> 
{
    public RegUserCommandInput Input { get; }
    public RegUserCommand(RegUserCommandInput input)
    {
        Input = input;
    }    
    
}

public class RegUserCommandHandler : BaseCommandHandler<RegUserCommand, RegUserCommandOutput>
{
    private readonly IRepository _repository;
    private readonly IEventBus _eventBus;

    public RegUserCommandHandler(IRepository repository
        , IEventBus eventBus
    )
    {
        _repository = repository;
        _eventBus = eventBus;
    }

    public override async Task<RegUserCommandOutput> Handle(RegUserCommand request, CancellationToken cancellationToken)
    {
        var domainService = new AccountServiceAggregate(_repository);

        var account = AutoMapping.Mapper.Map<Account>(request.Input);
        var newUserId = await domainService.RegisterAccountAsync(account);
            

        //account registered successfully
        //push event to EventBus to be consumed by exchange subscribers ( e.g. UserService )
        var regEvent = AutoMapping.Mapper.Map<AccountRegisteredEvent>(request.Input);
        regEvent.UserId = newUserId;
        await _eventBus.publishEventAsync(regEvent);

        return new RegUserCommandOutput(newUserId);
    }
}