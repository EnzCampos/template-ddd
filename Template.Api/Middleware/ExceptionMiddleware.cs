using System.Net;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Exceptions;

namespace Template.Api.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context,Exception exception,
            ILogger<ExceptionMiddleware> logger)
        {
            var code = HttpStatusCode.InternalServerError;
            CustomErrorDetails problem = new();

            switch (exception)
            {
                case NotFoundException notFoundException:
                    code = HttpStatusCode.NotFound;
                    problem = new CustomErrorDetails() { 
                        Title = notFoundException.Message,
                        Status = (int)code,
                        Detail = notFoundException.InnerException?.Message,
                        Type = nameof(NotFoundException),
                    };
                    break;

                case BadRequestException badRequestException:
                    code = HttpStatusCode.BadRequest;
                    problem = new CustomErrorDetails()
                    {
                        Title = badRequestException.Message,
                        Status = (int)code,
                        Detail = badRequestException.InnerException?.Message,
                        Type = nameof(BadRequestException),
                        Errors = badRequestException?.ValidationErrors,
                    };
                    break;

                case ForbiddenException forbiddenException:
                    code = HttpStatusCode.Forbidden;
                    problem = new CustomErrorDetails()
                    {
                        Title = forbiddenException.Message,
                        Status = (int)code,
                        Detail = forbiddenException.InnerException?.Message,
                        Type = nameof(ForbiddenException),
                    };
                    break;

                default:
                    code = HttpStatusCode.InternalServerError;
                    problem = new CustomErrorDetails()
                    {
                        Title = exception.Message,
                        Status = (int)code,
                        Message = exception?.InnerException?.Message ?? "",
                        Detail = exception?.StackTrace,
                        Type = nameof(Exception),
                    };

                    break;
            }

            logger.LogError(exception, exception?.Message);
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsJsonAsync(problem);
        }


        private class CustomErrorDetails : ProblemDetails
        {
            public string Message { get; set; } = string.Empty;
            public IDictionary<string, string[]>? Errors { get; set; }
        }
    }
}
