
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
        private readonly ISender _mediatorSender;

        public AuthenticationController(
            ISender mediatorSender
            )
        {
            _mediatorSender = mediatorSender;
        }

        [HttpPost()]
        public async Task<ActionResult> RegisterAccountAsync(RegUserCommandInput input)
        {
            await _mediatorSender.Send(new RegUserCommand(input));
            return Ok();
        }
    }
}