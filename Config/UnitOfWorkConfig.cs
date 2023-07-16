using HyperProf.Core.UOW;

namespace HyperProf.Config;

public static class UnitOfWorkConfig
{
    public static void RegisterUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}