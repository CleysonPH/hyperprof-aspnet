using HyperProf.Api.Students.Dtos;
using HyperProf.Api.Students.Mappers;
using HyperProf.Core.Services.Authentication;
using HyperProf.Core.UOW;

namespace HyperProf.Api.Students.Services;

public class TeacherStudentsService : ITeacherStudentsService
{
    private readonly IHyperprofAuthenticationService _authenticationService;
    private readonly IUnitOfWork _uow;
    private readonly IStudentMapper _studentMapper;

    public TeacherStudentsService(
        IHyperprofAuthenticationService authenticationService,
        IUnitOfWork uow,
        IStudentMapper studentMapper)
    {
        _authenticationService = authenticationService;
        _uow = uow;
        _studentMapper = studentMapper;
    }

    public IEnumerable<StudentResponse> GetStudentsByAuthenticatedTeacher()
    {
        var teacher = _authenticationService.GetAuthenticatedUser();
        return _uow.Students.Search(s => s.TeacherId == teacher.Id)
            .Select(_studentMapper.ToStudentResponse);
    }
}