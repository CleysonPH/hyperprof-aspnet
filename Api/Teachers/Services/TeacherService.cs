using FluentValidation;
using HyperProf.Api.Teachers.Dtos;
using HyperProf.Api.Teachers.Mappers;
using HyperProf.Core.Exceptions;
using HyperProf.Core.Services.Authentication;
using HyperProf.Core.Services.PasswordEncoder;
using HyperProf.Core.UOW;

namespace HyperProf.Api.Teachers.Services;

public class TeacherService : ITeacherService
{
    private readonly IUnitOfWork _uow;
    private readonly ITeacherMapper _teacherMapper;
    private readonly IValidator<TeacherRequest> _teacherRequestValidator;
    private readonly IPasswordEncoderService _passwordEncoderService;
    private readonly IHyperprofAuthenticationService _hyperprofAuthenticationService;

    public TeacherService(
        IUnitOfWork uow,
        ITeacherMapper teacherMapper,
        IValidator<TeacherRequest> teacherRequestValidator,
        IPasswordEncoderService passwordEncoderService,
        IHyperprofAuthenticationService hyperprofAuthenticationService)
    {
        _uow = uow;
        _teacherMapper = teacherMapper;
        _teacherRequestValidator = teacherRequestValidator;
        _passwordEncoderService = passwordEncoderService;
        _hyperprofAuthenticationService = hyperprofAuthenticationService;
    }

    public TeacherResponse Create(TeacherRequest teacherRequest)
    {
        var teacher = _teacherMapper.ToTeacher(teacherRequest);
        _teacherRequestValidator.ValidateAndThrow(teacherRequest);
        teacher.Password = _passwordEncoderService.Encode(teacherRequest.Password);
        _uow.BeginTransaction();
        _uow.Teachers.Add(teacher);
        _uow.Commit();
        return _teacherMapper.ToTeacherResponse(teacher);
    }

    public void Delete()
    {
        var teacher = _hyperprofAuthenticationService.GetAuthenticatedUser();
        _uow.BeginTransaction();
        _uow.Teachers.Delete(teacher);
        _uow.Commit();
    }

    public TeacherResponse GetById(int id)
    {
        var teacher = _uow.Teachers.GetById(id);
        if (teacher == null)
        {
            throw TeacherNotFoundException.WithId(id);
        }
        return _teacherMapper.ToTeacherResponse(teacher);
    }

    public IEnumerable<TeacherResponse> Search(string? q)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            return _uow.Teachers.GetAll()
                .Select(x => _teacherMapper.ToTeacherResponse(x));
        }
        return _uow.Teachers.Search(x => x.Description.ToLower().Contains(q.ToLower()))
            .Select(x => _teacherMapper.ToTeacherResponse(x));
    }

    public TeacherResponse Update(TeacherRequest teacherRequest)
    {
        var teacher = _hyperprofAuthenticationService.GetAuthenticatedUser();
        teacherRequest.Id = teacher.Id;
        _teacherRequestValidator.ValidateAndThrow(teacherRequest);
        _teacherMapper.ToTeacher(teacherRequest, teacher);
        teacher.Password = _passwordEncoderService.Encode(teacher.Password);
        _uow.BeginTransaction();
        _uow.Teachers.Update(teacher);
        _uow.Commit();
        return _teacherMapper.ToTeacherResponse(teacher);
    }
}