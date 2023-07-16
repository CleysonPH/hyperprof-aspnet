using HyperProf.Api.Teachers.Dtos;
using HyperProf.Core.Models;

namespace HyperProf.Api.Teachers.Mappers;

public class TeacherMapper : ITeacherMapper
{
    public Teacher ToTeacher(TeacherRequest teacherRequest)
    {
        return new Teacher
        {
            Name = teacherRequest.Nome,
            Email = teacherRequest.Email,
            Age = teacherRequest.Idade,
            Description = teacherRequest.Descricao,
            HourlyPrice = teacherRequest.ValorHora,
            Password = teacherRequest.Password
        };
    }

    public void ToTeacher(TeacherRequest teacherRequest, Teacher teacher)
    {
        teacher.Name = teacherRequest.Nome;
        teacher.Email = teacherRequest.Email;
        teacher.Age = teacherRequest.Idade;
        teacher.Description = teacherRequest.Descricao;
        teacher.HourlyPrice = teacherRequest.ValorHora;
        teacher.Password = teacherRequest.Password;
    }

    public TeacherResponse ToTeacherResponse(Teacher teacher)
    {
        return new TeacherResponse
        {
            Id = teacher.Id,
            Nome = teacher.Name,
            Email = teacher.Email,
            Idade = teacher.Age,
            Descricao = teacher.Description,
            ValorHora = teacher.HourlyPrice,
            FotoPerfil = teacher.ProfilePicture,
            CreatedAt = teacher.CreatedAt,
            UpdatedAt = teacher.UpdatedAt
        };
    }
}