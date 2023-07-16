using HyperProf.Api.Common.Middlewares;

namespace HyperProf.Config;

public static class MiddlewaresConfig
{
    public static void RegisterMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}