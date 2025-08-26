using System.Net;
using System.Text.Json;
using Bmg.Application.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Bmg.Api.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment env)
{
    private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IWebHostEnvironment _env = env ?? throw new ArgumentNullException(nameof(env));

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

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        string title = "Erro inesperado";
        string detail = exception.Message;

        switch (exception)
        {
            case ValidationException validationEx:
                statusCode = HttpStatusCode.BadRequest;
                title = "Erro de validação";
                detail = string.Join("; ", validationEx.Errors.Select(e => e.ErrorMessage));
                break;

            case NotFoundException notFoundEx:
                statusCode = HttpStatusCode.NotFound;
                title = "Recurso não encontrado";
                detail = notFoundEx.Message;
                break;

            case BusinessErrorException businessEx:
                statusCode = HttpStatusCode.UnprocessableEntity;
                title = "Erro de negócio";
                detail = businessEx.Message;
                break;

            case UnauthorizedAccessException unauthorizedEx:
                statusCode = HttpStatusCode.Unauthorized;
                title = "Não autorizado";
                detail = unauthorizedEx.Message;
                break;

            case ForbiddenException forbiddenEx:
                statusCode = HttpStatusCode.Forbidden;
                title = "Acesso proibido";
                detail = forbiddenEx.Message;
                break;
        }

        if (statusCode == HttpStatusCode.InternalServerError)
            logger.LogError(exception, "Erro interno: {Message}", exception.Message);

        var problemDetails = new ProblemDetails
        {
            Type = $"https://httpstatuses.com/{(int)statusCode}",
            Title = title,
            Status = (int)statusCode,
            Detail = _env.IsDevelopment() ? detail : string.Empty,
            Instance = context.Request.Path
        };

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;

        var json = JsonSerializer.Serialize(problemDetails);
        await context.Response.WriteAsync(json);
    }
}
