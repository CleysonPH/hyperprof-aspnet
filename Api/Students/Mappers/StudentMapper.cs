using HyperProf.Api.Students.Dtos;
using HyperProf.Core.Models;

namespace HyperProf.Api.Students.Mappers;

public class StudentMapper : IStudentMapper
{
    public Student ToStudent(StudentRequest studentRequest)
    {
        return new Student
        {
            Name = studentRequest.Nome,
            Email = studentRequest.Email,
            ClassDate = studentRequest.DataAula
        };
    }

    public StudentResponse ToStudentResponse(Student student)
    {
        return new StudentResponse
        {
            Id = student.Id,
            Nome = student.Name,
            Email = student.Email,
            DataAula = student.ClassDate,
            CreatedAt = student.CreatedAt,
            UpdatedAt = student.UpdatedAt
        };
    }
}