using Microsoft.AspNetCore.Http.Features;
using Template.Api.Attributes;
using Template.Infrastructure.Services.UnitOfWork;

public class TransactionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        if (httpContext.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
        {
            await _next(httpContext);
            return;
        }

        var endpoint = httpContext.Features.Get<IEndpointFeature>()?.Endpoint;
        var attribute = endpoint?.Metadata.GetMetadata<IgnoreGlobalTransactionsAttribute>();

        if (attribute is not null)
        {
            await _next(httpContext);
            return;
        }

        var currentTransaction = UnitOfWork.CreateScopedTransactionStatic();

        httpContext.Response.OnCompleted(() =>
        {
            try
            {
                if (httpContext.Response.StatusCode >= 200 && httpContext.Response.StatusCode < 300)
                {
                    currentTransaction?.Complete();
                }
            }
            finally
            {
                currentTransaction?.Dispose();
            }

            return Task.CompletedTask;
        });

        await _next(httpContext);
    }
}
