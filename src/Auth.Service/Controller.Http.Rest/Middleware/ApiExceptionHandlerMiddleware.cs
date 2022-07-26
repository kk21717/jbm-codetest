using Domain.Services.Exceptions;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Controller.Http.Rest.Middleware
{
    internal class ApiExceptionHandlerMiddleware
    {
        internal sealed class ErrorMessage
        {
            public string Message { get; set; }

            public ErrorMessage(string message)
            {
                Message = message;
            }
        }

        private readonly ILogger<ApiExceptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly RequestDelegate _next;

        public ApiExceptionHandlerMiddleware(
            RequestDelegate next, 
            ILogger<ApiExceptionHandlerMiddleware> logger, 
            IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                var errorMessage = await HandleExceptionAsync(httpContext, exception);

                //if (exception is not DomainException)
                    //_logger.LogCritical();
            }
        }

        private Task<ErrorMessage> HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorMessage = new ErrorMessage(exception.GetType().Name);

            switch (exception)
            {
                case InvalidPhoneException _:
                    context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                    break;

                case InvalidEmailException _:
                    context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                    break;

                case DuplicatePhoneException _:
                    context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    break;

                case RepositorayFailedException _:
                    context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorMessage.Message = "UnHandledException";
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.WriteAsync(JsonConvert.SerializeObject(errorMessage));

            return Task.FromResult(errorMessage);
        }

        
    }
}
