using HyperProf.Api.Teachers.Dtos;

namespace HyperProf.Api.Teachers.Services;

public interface IMeService
{
    TeacherResponse GetMe();
}