
using Application.Command.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Http.Rest.Controllers;

[ApiController]
[Route("auth-service")]
public class AuthenticationController : ControllerBase
{
    private readonly ISender _mediatorSender;

    public AuthenticationController(
        ISender mediatorSender
    )
    {
        _mediatorSender = mediatorSender;
    }

    [Tags("UserAccount")]
    [Route("reg-account")]
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
        var commandOutput = await _mediatorSender.Send(new RegUserCommand(input));

        //context.Response.ContentType = "application/json";
        //context.Response.WriteAsync(JsonConvert.SerializeObject(errorMessage));
        //return Task.FromResult(errorMessage);

        return Ok(commandOutput);
    }
}