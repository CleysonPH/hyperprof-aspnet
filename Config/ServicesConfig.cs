using HyperProf.Api.Auth.Services;
using HyperProf.Api.Students.Services;
using HyperProf.Api.Teachers.Services;
using HyperProf.Config.Properties;
using HyperProf.Core.Services.Authentication;
using HyperProf.Core.Services.Jwt;
using HyperProf.Core.Services.PasswordEncoder;
using HyperProf.Core.Services.Storage;

namespace HyperProf.Config;

public static class ServicesConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IMeService, MeService>();
        services.AddScoped<ITeacherStudentsService, TeacherStudentsService>();
        services.AddScoped<IProfileImageService, ProfileImageService>();
        services.AddScoped<IPasswordEncoderService, BCryptPasswordEncoderService>();
        services.AddScoped<IHyperprofAuthenticationService, HyperprofAuthenticationService>();
        services.AddScoped<IJwtService, JwtService>();

        var storageProvider = configuration.GetValue<StorageProviderOptions>("Storage:Provider");
        switch (storageProvider)
        {
            case StorageProviderOptions.Local:
                services.AddScoped<IStorageService, LocalStorageService>();
                break;
            case StorageProviderOptions.S3:
                services.AddScoped<IStorageService, S3StorageService>();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}