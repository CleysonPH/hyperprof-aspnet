using FluentValidation;
using HyperProf.Api.Auth.Dtos;
using HyperProf.Core.Services.Authentication;
using HyperProf.Core.Services.Jwt;

namespace HyperProf.Api.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IHyperprofAuthenticationService _hyperprofAuthenticationService;
    private readonly IValidator<LoginRequest> _loginRequestValidator;
    private readonly IJwtService _jwtService;
    private readonly IValidator<RefreshRequest> _refreshRequestValidator;

    public AuthService(
        IHyperprofAuthenticationService hyperprofAuthenticationService,
        IValidator<LoginRequest> loginRequestValidator,
        IJwtService jwtService,
        IValidator<RefreshRequest> refreshRequestValidator)
    {
        _hyperprofAuthenticationService = hyperprofAuthenticationService;
        _loginRequestValidator = loginRequestValidator;
        _jwtService = jwtService;
        _refreshRequestValidator = refreshRequestValidator;
    }

    public TokenResponse Login(LoginRequest loginRequest)
    {
        _loginRequestValidator.ValidateAndThrow(loginRequest);
        var teacher = _hyperprofAuthenticationService.Authenticate(loginRequest.Email, loginRequest.Password);
        return new TokenResponse
        {
            Token = _jwtService.GenerateToken(teacher),
            RefreshToken = _jwtService.GenerateRefreshToken(teacher),
        };
    }

    public void Logout(RefreshRequest refreshToken)
    {
        _refreshRequestValidator.ValidateAndThrow(refreshToken);
        _jwtService.InvalidateToken(refreshToken.RefreshToken);
    }

    public TokenResponse RefreshToken(RefreshRequest refreshToken)
    {
        _refreshRequestValidator.ValidateAndThrow(refreshToken);
        var teacher = _hyperprofAuthenticationService.Authenticate(refreshToken.RefreshToken);
        return new TokenResponse
        {
            Token = _jwtService.GenerateToken(teacher),
            RefreshToken = _jwtService.GenerateRefreshToken(teacher),
        };
    }
}