using HyperProf.Config.Properties;

namespace HyperProf.Config;

public static class PropertiesConfig
{
    public static void RegisterConfigProperties(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtConfigProperties>(configuration.GetSection("Jwt"));
        services.Configure<S3ConfigProperties>(configuration.GetSection("Storage:S3"));
        services.Configure<LocalStorageConfigProperties>(configuration.GetSection("Storage:Local"));
    }
}