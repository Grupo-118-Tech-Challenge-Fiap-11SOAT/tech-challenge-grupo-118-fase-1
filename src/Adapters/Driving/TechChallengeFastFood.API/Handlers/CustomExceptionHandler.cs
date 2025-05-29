using Domain;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TechChallengeFastFood.API.Handlers;

/// <summary>
/// Handles exceptions thrown during request processing in the application.
/// </summary>
/// <remarks>
/// This handler intercepts exceptions, logs them, and generates a standardized response to the client.
/// It utilizes the ILogger service for logging and processes exceptions to provide appropriate HTTP status codes
/// and problem details based on exception types.
/// </remarks>
public class CustomExceptionHandler : IExceptionHandler
{
    private readonly ILogger<CustomExceptionHandler> _logger;

    public CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Handles an exception asynchronously by logging it and generating an appropriate HTTP response.
    /// </summary>
    /// <param name="httpContext">The HTTP context representing the current request and response.</param>
    /// <param name="exception">The exception that occurred during the request processing.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation. Returns <c>true</c> if the exception was handled; otherwise, <c>false</c>.</returns>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogWarning(exception, "An {ExceptionType} was thrown", exception.GetType().Name);

        int statusCode = exception switch
        {
          Application.ApplicationException => StatusCodes.Status400BadRequest,
            DomainException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = "An error occured",
            Detail = exception.Message.Trim()
        };

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);

        // Return true to signal that this exception is handled
        // Return false to continue with the default behavior
        return true;
    }
}