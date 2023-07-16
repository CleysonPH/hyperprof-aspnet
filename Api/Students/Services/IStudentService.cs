using HyperProf.Api.Students.Dtos;

namespace HyperProf.Api.Students.Services;

public interface IStudentService
{
    StudentResponse Create(int teacherId, StudentRequest studentRequest);
}