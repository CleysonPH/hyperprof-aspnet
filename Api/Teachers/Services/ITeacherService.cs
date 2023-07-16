using HyperProf.Api.Teachers.Dtos;

namespace HyperProf.Api.Teachers.Services;

public interface ITeacherService
{
    TeacherResponse Create(TeacherRequest teacherRequest);
    IEnumerable<TeacherResponse> Search(string? q);
    TeacherResponse GetById(int id);
    void Delete();
    TeacherResponse Update(TeacherRequest teacherRequest);
}