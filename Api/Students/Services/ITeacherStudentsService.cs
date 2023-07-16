using HyperProf.Api.Students.Dtos;

namespace HyperProf.Api.Students.Services;

public interface ITeacherStudentsService
{
    IEnumerable<StudentResponse> GetStudentsByAuthenticatedTeacher();
}