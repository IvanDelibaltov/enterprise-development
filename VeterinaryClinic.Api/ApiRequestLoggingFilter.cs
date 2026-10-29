using Microsoft.AspNetCore.Mvc.Filters;

namespace VeterinaryClinic.Api;

/// <summary>Обеспечивает работу API клиники</summary>
public sealed class ApiRequestLoggingFilter(ILogger<ApiRequestLoggingFilter> logger) : IActionFilter
{
    /// <inheritdoc/>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        logger.LogInformation("Executing {Action}", context.ActionDescriptor.DisplayName);
    }

    /// <inheritdoc/>
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is not null)
            logger.LogWarning(context.Exception, "Action {Action} failed", context.ActionDescriptor.DisplayName);
        else
            logger.LogInformation("Completed {Action}", context.ActionDescriptor.DisplayName);
    }
}
