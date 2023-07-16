using HyperProf.Config.Properties;

namespace HyperProf.Config;

public static class AuthenticationConfig
{
    public static void RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfigProperties = configuration.GetSection("Jwt").Get<JwtConfigProperties>();
        if (jwtConfigProperties is null)
        {
            throw new Exception("Jwt configuration is missing");
        }
        services.AddAuthorization();
        services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = jwtConfigProperties.GetTokenValidationParameters();
            });
    }
}