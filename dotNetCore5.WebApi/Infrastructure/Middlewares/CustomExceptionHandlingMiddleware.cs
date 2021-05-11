using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace dotNetCore5.WebApi.Infrastructure.Middlewares
{
    public class CustomExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<CustomExceptionHandlingMiddleware> logger;

        public CustomExceptionHandlingMiddleware
        (
            RequestDelegate next,
            ILogger<CustomExceptionHandlingMiddleware> logger
        )
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);
            }
            catch (Exception ex)
            {
                var statusCode = StatusCodes.Status500InternalServerError;

                //log ex
                logger.LogError(statusCode, ex, ex?.Message);

                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = statusCode;

                var errorResponse = new
                {
                    response.StatusCode,
                    ex?.Message
                };

                var errorJson = JsonSerializer.Serialize(errorResponse);

                await response.WriteAsync(errorJson);
            }
        }
    }

    public static class ExceptionHandleMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlingMiddleware>();
        }
    }
}
