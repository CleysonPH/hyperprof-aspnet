using HyperProf.Api.Students.Dtos;
using HyperProf.Core.Models;

namespace HyperProf.Api.Students.Mappers;

public interface IStudentMapper
{
    StudentResponse ToStudentResponse(Student student);
    Student ToStudent(StudentRequest studentRequest);
}