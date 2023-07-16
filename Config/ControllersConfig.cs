using HyperProf.Core.Utils.NamingPolicies;

namespace HyperProf.Config;

public static class ControllersConfig
{
    public static void RegisterControllers(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
                {
                    options
                        .JsonSerializerOptions
                        .PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance;
                }
            );
    }
}