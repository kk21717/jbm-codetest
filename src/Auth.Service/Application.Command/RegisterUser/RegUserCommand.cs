
using Application.Command.Events;
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
    //
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

        //public RegUserCommandHandler() { } // => _fakeDataStore = fakeDataStore;

        readonly IRepository _repository;
        readonly IEventBus _eventBus;
        //readonly IPublishEndpoint _publishEvent;

        public RegUserCommandHandler(IRepository repository
            , IEventBus eventBus
            //, IPublishEndpoint publishEvent
            )
        {
            _repository = repository;
            _eventBus = eventBus;
            //_publishEvent = publishEvent;
        }

        //public async Task<Unit> Handle(RegUserCommand request, CancellationToken cancellationToken)
        //{
            
        //}

        public override async Task<Unit> Handle(RegUserCommand request, CancellationToken cancellationToken)
        {
            var domainService = new RegisterAccountService(_repository
                //,_eventBus
                );

            await domainService.RegisterAccountAsync(new Account(request.Input.Phone, request.Input.Email));
            //await domainService.RegisterAccountAsync(new Account("", ""));

            //account registered successfully
            //push event to eventbus be consumed by exchange subscribers ( e.g. UserService )
            var regEvent = new AccountRegisteredEvent(
                    phone: request.Input.Phone,
                    email: request.Input.Email,
                    firstName: request.Input.FirstName,
                    lastName: request.Input.LastName
                );
            await _eventBus.publishEventAsync<AccountRegisteredEvent>(regEvent);

            return Unit.Value;
        }
    }

}
