
using Application.Query.GetUserProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    [Route("get/{userId:int}")]
    public async Task<ActionResult> GetUserProfileAsync(int userId)
    {
        var profile = await _mediatorSender.Send(new GetUserProfileQuery(userId));
        return Ok(profile);
    }
}