
using Application.Command.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Http.Rest.Controllers;

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

    [HttpPost]
    public async Task<ActionResult> RegisterAccountAsync(RegUserCommandInput input)
    {
        input = new RegUserCommandInput()
        {
            Email = "test@test.com",
            Phone = "+98912" + new Random().Next(1000000, 9999999),
            FirstName = "kamran",
            LastName = "karami"
        };
        await _mediatorSender.Send(new RegUserCommand(input));
        return Ok();
    }
}