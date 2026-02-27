using CapitalSync.Application.DTOs.Errors;
using CapitalSync.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CapitalSync.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is CapitalSyncException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknownError(context);
        }
    }

    private static void HandleProjectException(ExceptionContext context)
    {
        var capitalSyncException = context.Exception as CapitalSyncException;
        var errorResponse = new ResponseErrorJson(capitalSyncException!.GetErrors());

        context.HttpContext.Response.StatusCode = capitalSyncException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    private static void ThrowUnknownError(ExceptionContext context)
    {
        var exception = context.Exception;

        Console.WriteLine("Erro inesperado: " + exception + " ASS: J. Ricardo");

        var errorResponse = new ResponseErrorJson("Unknown error.");

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}