using System.Security.Claims;
using HyperProf.Core.Exceptions;
using HyperProf.Core.Models;
using HyperProf.Core.Services.Jwt;
using HyperProf.Core.Services.PasswordEncoder;
using HyperProf.Core.UOW;

namespace HyperProf.Core.Services.Authentication;

public class HyperprofAuthenticationService : IHyperprofAuthenticationService
{
    private readonly IUnitOfWork _uow;
    private readonly IPasswordEncoderService _passwordEncoderService;
    private readonly IJwtService _jwtService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HyperprofAuthenticationService(
        IUnitOfWork uow,
        IPasswordEncoderService passwordEncoderService,
        IJwtService jwtService,
        IHttpContextAccessor httpContextAccessor)
    {
        _uow = uow;
        _passwordEncoderService = passwordEncoderService;
        _jwtService = jwtService;
        _httpContextAccessor = httpContextAccessor;
    }

    public Teacher Authenticate(string email, string password)
    {
        var teacher = _uow.Teachers.Search(x => x.Email == email).SingleOrDefault();
        if (teacher == null || !_passwordEncoderService.Verify(password, teacher.Password))
        {
            throw new InvalidCredentialsException();
        }
        return teacher;
    }

    public Teacher Authenticate(string refreshToken)
    {
        _jwtService.ValidateToken(refreshToken);
        var email = _jwtService.GetEmailFromRefreshToken(refreshToken);
        var teacher = _uow.Teachers.Search(x => x.Email == email).SingleOrDefault();
        if (teacher == null)
        {
            throw new InvalidCredentialsException();
        }
        _jwtService.InvalidateToken(refreshToken);
        return teacher;
    }

    public Teacher GetAuthenticatedUser()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || user.Claims == null)
        {
            throw new UnauthenticatedException();
        }
        var email = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        if (email == null)
        {
            throw new UnauthenticatedException();
        }
        var teacher = _uow.Teachers.Search(x => x.Email == email).SingleOrDefault();
        if (teacher == null)
        {
            throw new UnauthenticatedException();
        }
        return teacher;
    }
}