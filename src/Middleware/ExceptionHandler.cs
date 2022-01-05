using System;
using System.Net;
using System.Threading.Tasks;
using BakuchiApi.StatusExceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BakuchiApi.Middleware
{
    public static class ExceptionHandlerMiddleware
    {
        public static void
            UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.Use(HandleExceptionResponse);
        }

        private static async Task
            HandleExceptionResponse(HttpContext httpContext, Func<Task> next)
        {
            var exceptionDetails = httpContext.Features
                .Get<IExceptionHandlerFeature>();
            var response = httpContext.Response;
            var ex = exceptionDetails?.Error;

            response.ContentType = "application/json";

            switch (ex)
            {
                case BaseServiceException:
                    break;
                default:
                    response.StatusCode =
                        (int) HttpStatusCode.InternalServerError;
                    break;
            }

            await response.WriteAsJsonAsync(new ProblemDetails
            {
                Title = response.StatusCode.ToString(),
                Status = response.StatusCode,
                Instance = httpContext.Request.Path,
                Detail = ex?.Message
            });
        }
    }
}