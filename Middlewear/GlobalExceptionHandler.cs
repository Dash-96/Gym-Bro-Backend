using GymBroAspBackend.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace GymBroAspBackend.Middlewear;
public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken ct)
    {
        var (status, title) = exception switch
        {
            InvalidCredentialsException   => (StatusCodes.Status401Unauthorized, "Invalid credentials"),
            RefreshTokenExpiredException  => (StatusCodes.Status401Unauthorized, "Session expired"),
            _                             => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
        };

        if (status == 500)
            _logger.LogError(exception, "Unhandled exception");

        context.Response.StatusCode = status;
        await context.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = status,
            Title = title
        }, ct);

        return true;
    }
}

