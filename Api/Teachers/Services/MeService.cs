using HyperProf.Api.Teachers.Dtos;
using HyperProf.Api.Teachers.Mappers;
using HyperProf.Core.Services.Authentication;

namespace HyperProf.Api.Teachers.Services;

public class MeService : IMeService
{
    private readonly IHyperprofAuthenticationService _authenticationService;
    private readonly ITeacherMapper _teacherMapper;

    public MeService(
        IHyperprofAuthenticationService authenticationService,
        ITeacherMapper teacherMapper)
    {
        _authenticationService = authenticationService;
        _teacherMapper = teacherMapper;
    }

    public TeacherResponse GetMe()
    {
        var teacher = _authenticationService.GetAuthenticatedUser();
        return _teacherMapper.ToTeacherResponse(teacher);
    }
}