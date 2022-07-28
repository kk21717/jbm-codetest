
using Application.Command.RegisterUser;
using Application.Query.GetUserProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Controller.Http.Rest.Controllers;

[ApiController]
[Route("profile")]
public class UserProfileController : ControllerBase
{
    private readonly ISender _mediatorSender;

    public UserProfileController(
        ISender mediatorSender
    )
    {
        _mediatorSender = mediatorSender;
    }

    [HttpGet]
    [Route("get/{userId}")]
    public async Task<ActionResult> GetUserProfileAsync(int userId)
    {
        var profile = await _mediatorSender.Send(new GetUserProfileQuery(userId));
        return Ok(profile);
    }
}