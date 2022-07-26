
using Application.Command.Events;
using Application.Command.RegisterUser;
using AutoMapper;
using Domain.Entities;
using Domain.Services;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Lib.EventBus;

namespace Controller.Http.Rest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IRepository _repository;
        private readonly ISender _mediatorSender;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IEventBus _eventBus;
        private readonly IMapper _mapper;

        public AuthenticationController(
            ILogger<AuthenticationController> logger
            ,IRepository repository
            ,ISender mediatorSender
            ,IPublishEndpoint publishEndpoint
            ,IEventBus eventBus
            ,IMapper mapper
            )
        {
            _logger = logger;
            _repository = repository;
            _publishEndpoint = publishEndpoint;
            _mediatorSender = mediatorSender;
            _eventBus = eventBus;
            _mapper = mapper;
        }

        [HttpPost()]
        public async Task<ActionResult> RegisterAccountAsync(RegUserCommandInput input)
        {



            await _mediatorSender.Send(new RegUserCommand(input));

            return Ok();
        }
    }
}