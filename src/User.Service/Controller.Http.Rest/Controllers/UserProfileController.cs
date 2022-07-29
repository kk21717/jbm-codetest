
using Application.Query.GetUserProfile;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Http.Rest.Controllers;

[ApiController]
[Route("user-api")]
public class UserProfileController : ControllerBase
{
    private readonly ISender _mediatorSender;

    public UserProfileController(
        ISender mediatorSender
    )
    {
        _mediatorSender = mediatorSender;
    }

    [Tags("UserProfile")]
    [Route("profiles/{userId:int}")]
    [HttpGet]
    public async Task<UserProfile> GetUserProfileAsync(int userId)
    {
        var profile = await _mediatorSender.Send(new GetUserProfileQuery(userId));
        return profile; 
        //returning domain object directly (instead of a dto) for saving time :)
    }
}