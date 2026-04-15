using System.Net;
using System.Text.Json;
using QuantityMeasurementAppBusinessLayer.Exceptions;

namespace QuantityMeasurementAPI.Middleware;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        // 1. Determine the status code FIRST
        int statusCode = (int)HttpStatusCode.InternalServerError;

        if (exception is BaseBusinessLayerException businessEx)
        {
            statusCode = businessEx.StatusCode;
        }

        // 2. Set the response status code BEFORE creating the response object
        context.Response.StatusCode = statusCode;

        // 3. Create the response
        var response = new
        {
            StatusCode = statusCode,
            Message = (statusCode == 500) ? "Internal Server Error" : "Business Logic Error",
            Detailed = exception.Message
        };

        // 4. Serialize and write
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}