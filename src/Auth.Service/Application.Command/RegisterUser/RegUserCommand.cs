
using Application.Command.Events;
using Application.Command.Mapping;
using AutoMapper;
using Domain.Entities;
using Domain.Services;
using MediatR;
using Shared.Lib.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.RegisterUser
{
    
    public class RegUserCommand : BaseCommand<Unit> 
    {
        public RegUserCommandInput Input { get; }
        public RegUserCommand(RegUserCommandInput input)
        {
            Input = input;
        }    
    
    }

    public class RegUserCommandHandler : BaseCommandHandler<RegUserCommand, Unit>
    {
        readonly IRepository _repository;
        readonly IEventBus _eventBus;

        public RegUserCommandHandler(IRepository repository
            , IEventBus eventBus
            )
        {
            _repository = repository;
            _eventBus = eventBus;
        }

        public override async Task<Unit> Handle(RegUserCommand request, CancellationToken cancellationToken)
        {
            var domainService = new RegisterAccountService(_repository);

            var account = AutoMapping.Mapper.Map<Account>(request.Input);
            await domainService.RegisterAccountAsync(account);
            

            //account registered successfully
            //push event to eventbus to be consumed by exchange subscribers ( e.g. UserService )
            var regEvent = AutoMapping.Mapper.Map<AccountRegisteredEvent>(request.Input);
            await _eventBus.publishEventAsync<AccountRegisteredEvent>(regEvent);

            return Unit.Value;
        }
    }

}
