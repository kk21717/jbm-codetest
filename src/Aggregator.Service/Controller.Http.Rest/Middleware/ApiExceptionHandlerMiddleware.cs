using Controller.Http.Rest.Exceptions;
using Newtonsoft.Json;
using System.Net;

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
        private readonly RequestDelegate _next;

        public ApiExceptionHandlerMiddleware(
            RequestDelegate next,
            ILogger<ApiExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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
                //_logger.LogCritical(errorMessage);
            }
        }

        private static Task<ErrorMessage> HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorMessage = new ErrorMessage(exception.GetType().Name);

            switch (exception)
            {
                case DownstreamException:
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
