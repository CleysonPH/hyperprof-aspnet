using HyperProf.Api.Students.Mappers;
using HyperProf.Api.Teachers.Mappers;

namespace HyperProf.Config;

public static class MappersConfig
{
    public static void RegisterMappers(this IServiceCollection services)
    {
        services.AddScoped<ITeacherMapper, TeacherMapper>();
        services.AddScoped<IStudentMapper, StudentMapper>();
    }
}