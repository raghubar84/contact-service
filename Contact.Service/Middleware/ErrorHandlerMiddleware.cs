using Contact.Service.Controllers;
using Contact.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Contact.Service.Middleware
{
    public class ErrorHandlerMiddleware(RequestDelegate _next, ILogger<ErrorHandlerMiddleware> logger)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                var problemDetails = new ProblemDetails
                {
                    Status = response.StatusCode,
                    Title = "Unexpected exception occured.",
                };
                logger.LogError(error, $"{nameof(ErrorHandlerMiddleware)}: Exception occured while proccessing request.");
                var result = JsonSerializer.Serialize(problemDetails);
                await response.WriteAsync(result);
            }
        }
    }
}
