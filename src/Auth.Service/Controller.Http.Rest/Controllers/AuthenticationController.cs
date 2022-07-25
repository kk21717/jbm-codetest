
using Application.Command.RegisterUser;
using Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Http.Rest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IRepository _repository;
        //private readonly IPublishEndpoint _publishEndpoint;

        private readonly ISender _mediatorSender;

        public AuthenticationController(ILogger<AuthenticationController> logger,
            IRepository repository
            //,IPublishEndpoint publishEndpoint
            , ISender mediatorSender
            )
        {
            _logger = logger;
            _repository = repository;
            //_publishEndpoint = publishEndpoint;
            _mediatorSender = mediatorSender;
        }

        [HttpPost()]
        public async Task<ActionResult> RegisterAccountAsync(RegUserCommandInput input)
        {
            await _mediatorSender.Send(new RegUserCommand(input));
            //await _mediatorSender.Send(new RegUserCommand());
            return Ok();
        }
    }
}