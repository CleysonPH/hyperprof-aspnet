using HyperProf.Api.Teachers.Dtos;
using HyperProf.Core.Models;

namespace HyperProf.Api.Teachers.Mappers;

public interface ITeacherMapper
{
    TeacherResponse ToTeacherResponse(Teacher teacher);
    Teacher ToTeacher(TeacherRequest teacherRequest);
    void ToTeacher(TeacherRequest teacherRequest, Teacher teacher);
}