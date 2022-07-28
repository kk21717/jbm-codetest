
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
        #region temp code to speed up manual testing
        if (input.Email == "string" && 
            input.Phone == "string" && 
            input.FirstName == "string" && 
            input.LastName == "string")
        {
            input = new RegUserCommandInput()
            {
                Email = "test@test.com",
                Phone = "+98912" + new Random().Next(1000000, 9999999),
                FirstName = "kamran",
                LastName = "karami"
            };
        }
        #endregion

        // execute command using mediatR
        var commandOutput = await _mediatorSender.Send(new RegUserCommand(input));

        // return 200 resonse code with having {newUserId} in response body
        return Ok(commandOutput);
    }
}