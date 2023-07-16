using FluentValidation;
using HyperProf.Api.Students.Dtos;
using HyperProf.Api.Students.Mappers;
using HyperProf.Core.Exceptions;
using HyperProf.Core.UOW;

namespace HyperProf.Api.Students.Services;

public class StudentService : IStudentService
{
    private readonly IUnitOfWork _uow;
    private readonly IStudentMapper _studentMapper;
    private readonly IValidator<StudentRequest> _studentRequestValidator;

    public StudentService(IUnitOfWork uow, IStudentMapper studentMapper, IValidator<StudentRequest> studentRequestValidator)
    {
        _uow = uow;
        _studentMapper = studentMapper;
        _studentRequestValidator = studentRequestValidator;
    }

    public StudentResponse Create(int teacherId, StudentRequest studentRequest)
    {
        _studentRequestValidator.ValidateAndThrow(studentRequest);
        var student = _studentMapper.ToStudent(studentRequest);
        _uow.BeginTransaction();
        var teacher = _uow.Teachers.GetById(teacherId);
        if (teacher == null)
        {
            _uow.Rollback();
            throw TeacherNotFoundException.WithId(teacherId);
        }
        student.Teacher = teacher;
        _uow.Students.Add(student);
        _uow.Commit();
        return _studentMapper.ToStudentResponse(student);
    }
}