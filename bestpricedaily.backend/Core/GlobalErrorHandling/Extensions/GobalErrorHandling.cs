using Core.GlobalErrorHandling.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using bestpricedaily;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;

namespace Core.GlobalErrorHandling.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        // public static void ConfigureExceptionHandler(this IApplicationBuilder app,IConfiguration config, ILoggerFactory logger )
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            // LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));
            // var logger = NLog.Web.NLogBuilder.ConfigureNLog(LogManager.Configuration).GetCurrentClassLogger();
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
                        logger.Error(($"5XX Server Error: {contextFeature.Error}").Substring(0,510));

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