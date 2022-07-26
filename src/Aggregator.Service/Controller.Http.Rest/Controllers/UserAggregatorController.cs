using Controller.Http.Rest.Aggregators;
using Controller.Http.Rest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Controller.Http.Rest.Util;

namespace Controller.Http.Rest.Controllers
{
    [ApiController]
    [Route("aggregator-api")]
    public class UserAggregatorController : ControllerBase
    {
        private readonly ILogger<UserAggregatorController> _logger;
        private readonly IUserAggregator _userAggregator;


        public UserAggregatorController(
            ILogger<UserAggregatorController> logger
            ,IUserAggregator userAggregator)
        {
            _logger = logger;
            _userAggregator = userAggregator;
        }


        [Tags("UserView")]
        [Route("userviews/{userId:int}")]
        [HttpGet]
        public async Task<UserView> GetUserView(int userId)
        {
            var userView = await _userAggregator.GetUserViewAsync(userId);
            return userView;
        }

       
    }
}