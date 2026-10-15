using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
        : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error Message {ExceptionMessage}, time of occurance {time}", exception.Message, DateTime.UtcNow);

            (string Detail, string Title, int statusCode) details = exception switch
            {

                InvalidServerException => (
                    exception.Message ?? string.Empty,
                    exception.GetType().Name,
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError
                ),
                ValidationException => (
                    exception.Message ?? string.Empty,
                    exception.GetType().Name,
                    context.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                NotFoundExceptions => (
                    exception.Message ?? string.Empty,
                    exception.GetType().Name,
                    context.Response.StatusCode = StatusCodes.Status404NotFound
                ),
                _ => (
                    exception.Message ?? string.Empty,
                    exception.GetType().Name,
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError
                )

            };
            var problemDetails = new ProblemDetails
            {
                Title = details.Title,
                Status = details.statusCode,
                Detail = details.Detail,
                Instance= context.Request.Path
            };
            problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
            if (exception is ValidationException validationException) {
                problemDetails.Extensions.Add("validationErrors", validationException.Errors);
            }
            await context.Response.WriteAsJsonAsync(problemDetails,cancellationToken:  cancellationToken);
            return true;
        }
    }
}
