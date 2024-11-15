using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace task2API.Errors
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            this.logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
           
            logger.LogError(exception, "Catch error: {ErrorMessage}", exception.Message);

            
            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server error",
                Detail = exception.Message,
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails);
            return true;
        }
    }

}
