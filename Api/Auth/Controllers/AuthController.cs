using HyperProf.Api.Auth.Dtos;
using HyperProf.Api.Auth.Services;
using HyperProf.Core.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HyperProf.Api.Auth.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        var token = _authService.Login(loginRequest);
        return Ok(token);
    }

    [HttpPost("refresh")]
    public IActionResult Refresh([FromBody] RefreshRequest refreshToken)
    {
        var token = _authService.RefreshToken(refreshToken);
        return Ok(token);
    }

    [HttpPost("logout")]
    public IActionResult Logout([FromBody] RefreshRequest refreshToken)
    {
        _authService.Logout(refreshToken);
        return this.ResetContent();
    }
}