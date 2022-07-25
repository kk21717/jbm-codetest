using Application.Command;
using Domain.Core;
using Domain.Core.Dto;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Service.RestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
    

        private readonly ILogger<AuthenticationController> _logger;
        private readonly IRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;

        public AuthenticationController(ILogger<AuthenticationController> logger,
            IRepository repository,
            IPublishEndpoint publishEndpoint
            )
        {
            _logger = logger;
            _repository = repository;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost()]
        public async Task<ActionResult> RegisterAccountAsync(RegisterAccountInput input)
        {
            //var command = new RegisterAccountCommand(_repository)
            //await;
            return Ok("hello " + input.FirstName + " " + input.LastName);

            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();
        }
    }
}