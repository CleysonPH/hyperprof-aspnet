using HyperProf.Core.Models;
using HyperProf.Core.Repositories;

namespace HyperProf.Config;

public static class RepositoriesConfig
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Teacher>, Repository<Teacher>>();
        services.AddScoped<IRepository<Student>, Repository<Student>>();
        services.AddScoped<IRepository<InvalidatedToken>, Repository<InvalidatedToken>>();
    }
}