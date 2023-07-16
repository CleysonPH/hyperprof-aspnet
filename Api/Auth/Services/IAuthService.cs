using HyperProf.Api.Auth.Dtos;

namespace HyperProf.Api.Auth.Services;

public interface IAuthService
{
    TokenResponse Login(LoginRequest loginRequest);
    TokenResponse RefreshToken(RefreshRequest refreshToken);
    void Logout(RefreshRequest refreshToken);
}