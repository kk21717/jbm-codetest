
using Application.Command.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Http.Rest.Controllers;

[ApiController]
[Route("[controller]")]
public class UserProfileController : ControllerBase
{
    private readonly ISender _mediatorSender;

    public UserProfileController(
        ISender mediatorSender
    )
    {
        _mediatorSender = mediatorSender;
    }

    //[HttpPost]
    //public async Task<ActionResult> RegisterProfileAsync(RegProfileCommandInput input)
    //{
    //    await _mediatorSender.Send(new RegProfileCommand(input));
    //    return Ok();
    //}
}