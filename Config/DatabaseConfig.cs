using HyperProf.Core.Data.Contexts;

namespace HyperProf.Config;

public static class DatabaseConfig
{
    public static void RegisterDatabase(this IServiceCollection services)
    {
        services.AddDbContext<HyperprofDbContext>();
    }
}