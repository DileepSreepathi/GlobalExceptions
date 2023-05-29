
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web.Helpers;

public class ExceptionMiddleware:IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    //private readonly RequestDelegate _next;
    private readonly IHostEnvironment _environment;
    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _logger = logger;

        _environment = env;

    }
    public async Task InvokeAsync(HttpContext context,RequestDelegate _next)
    {
        try {
            await _next(context);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context,Exception ex)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode =(int)HttpStatusCode.InternalServerError;
        var response = _environment.IsDevelopment() ?
            new CustomResponse(context.Response.StatusCode, ex.Message, ex.StackTrace.ToString())
            : new CustomResponse(context.Response.StatusCode, ex.Message, "Internal Server Error");
        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }

}