using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Api.Helpers;

public static class AppExceptionHandlers
{
    //private static ILogger _logger = MyFileLoggerFactory.CreateLogger("AppExceptionHandler");
    public static void AppExceptionHandler(this WebApplication app,ILogger logger) 
    {
        app.UseExceptionHandler(appError => 
        {
            appError.Run(async context => 
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    logger.LogError($"Something went wrong: {contextFeature.Error}");
                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "Internal Server Error.",
                    }.ToString());
                }
            });
        });
    }
}

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public override string ToString() => JsonSerializer.Serialize(this);
}

public static class ExceptionExtension
{
    public static string GetAllMessages(this System.Exception exception)
    {
        var ex = exception;
        var sb = new System.Text.StringBuilder();
        while (ex != null)
        {
            sb.AppendLine(ex.Message);
            ex = ex.InnerException;
        }
        return sb.ToString();
    }
}    
