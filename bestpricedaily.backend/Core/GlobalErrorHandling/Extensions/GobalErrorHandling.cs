using Core.GlobalErrorHandling.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using NLog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Core.GlobalErrorHandling.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            //since i don't know/lazy how to get the appsettings for logManager -> loglevel in appsettings won't overwrite it 
            var logger = LogManager.GetCurrentClassLogger();

            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        // logger.Error(($"5XX Server Error: {contextFeature.Error}").Substring(0,510));
                        logger.Error($"5XX Server Error: {contextFeature.Error}");

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }
    }
}