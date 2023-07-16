using System.Text.Json;
using FluentValidation;
using HyperProf.Api.Common.Dtos;
using HyperProf.Core.Exceptions;
using HyperProf.Core.Extensions;
using HyperProf.Core.Utils.NamingPolicies;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace HyperProf.Api.Common.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance
        };
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
            await handleAuthenticationFailedAsync(context);
        }
        catch (ValidationException ex)
        {
            await handleValidationExceptionAsync(context, ex);
        }
        catch (ModelNotFoundException ex)
        {
            await handleModelNotFoundExceptionAsync(context, ex);
        }
        catch (AuthenticationException ex)
        {
            await handleAuthenticationExceptionAsync(context, ex);
        }
        catch (JwtException ex)
        {
            await handleSecurityTokenExceptionAsync(context, ex);
        }
    }

    private Task handleAuthenticationFailedAsync(HttpContext context)
    {
        if (context.Response.StatusCode != 401)
        {
            return Task.CompletedTask;
        }
        var wwwAuthenticate = context.Response.Headers[HeaderNames.WWWAuthenticate];
        if (wwwAuthenticate == StringValues.Empty)
        {
            return Task.CompletedTask;
        }
        if (wwwAuthenticate.ToString() == "Bearer")
        {
            throw new UnauthenticatedException("É necessário informar um token de acesso válido");
        }
        if (wwwAuthenticate.ToString().Contains("The token expired"))
        {
            var token = context.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            throw JwtExpiredException.WithToken(token);
        }
        if (wwwAuthenticate.ToString().Contains("invalid_token"))
        {
            throw new JwtInvalidTokenException("O token informado é inválido");
        }
        return Task.CompletedTask;
    }

    private async Task handleSecurityTokenExceptionAsync(HttpContext context, JwtException ex)
    {
        var body = new ErrorResponse
        {
            Status = 401,
            Error = "Unauthorized",
            Cause = ex.GetType().Name,
            Message = ex.Message
        };
        context.Response.StatusCode = body.Status;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(body, _jsonSerializerOptions));
    }

    private async Task handleAuthenticationExceptionAsync(HttpContext context, AuthenticationException ex)
    {
        var body = new ErrorResponse
        {
            Status = 401,
            Error = "Unauthorized",
            Cause = ex.GetType().Name,
            Message = ex.Message
        };
        context.Response.StatusCode = body.Status;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(body, _jsonSerializerOptions));
    }

    private async Task handleModelNotFoundExceptionAsync(HttpContext context, ModelNotFoundException ex)
    {
        var body = new ErrorResponse
        {
            Status = 404,
            Error = "Not Found",
            Cause = ex.GetType().Name,
            Message = ex.Message
        };
        context.Response.StatusCode = body.Status;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(body, _jsonSerializerOptions));
    }

    private async Task handleValidationExceptionAsync(HttpContext context, ValidationException ex)
    {
        var body = new ValidationErrorResponse
        {
            Status = 400,
            Error = "Bad Request",
            Cause = ex.GetType().Name,
            Message = "Houveram erros de validação",
            Errors = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    group => group.Key.ToSnakeCase(),
                    group => group.Select(e => e.ErrorMessage).ToArray()
                )
        };
        context.Response.StatusCode = body.Status;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(body, _jsonSerializerOptions));
    }
}