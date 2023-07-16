using HyperProf.Core.Models;

namespace HyperProf.Core.Services.Jwt;

public interface IJwtService
{
    string GenerateToken(Teacher teacher);
    string GenerateRefreshToken(Teacher teacher);
    string GetEmailFromRefreshToken(string token);
    void InvalidateToken(string token);
    void ValidateToken(string token);
}