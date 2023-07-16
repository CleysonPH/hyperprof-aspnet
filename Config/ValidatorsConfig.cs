using FluentValidation;
using HyperProf.Api.Auth.Dtos;
using HyperProf.Api.Auth.Validators;
using HyperProf.Api.Students.Dtos;
using HyperProf.Api.Students.Validators;
using HyperProf.Api.Teachers.Dtos;
using HyperProf.Api.Teachers.Validators;

namespace HyperProf.Config;

public static class ValidatorsConfig
{
    public static void RegisterValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<TeacherRequest>, TeacherRequestValidator>();
        services.AddTransient<IValidator<StudentRequest>, StudentRequestValidator>();
        services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
        services.AddTransient<IValidator<RefreshRequest>, RefreshRequestValidator>();
        services.AddTransient<IValidator<ProfileImageRequest>, ProfileImageRequestValidator>();
    }
}