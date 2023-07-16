using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using HyperProf.Config.Properties;
using HyperProf.Core.Exceptions;
using HyperProf.Core.Models;
using HyperProf.Core.UOW;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HyperProf.Core.Services.Jwt;

public class JwtService : IJwtService
{
    private readonly JwtConfigProperties _jwtConfigProperties;
    private readonly IUnitOfWork _uow;

    public JwtService(
        IOptions<JwtConfigProperties> jwtConfigProperties,
        IUnitOfWork uow)
    {
        _jwtConfigProperties = jwtConfigProperties.Value;
        _uow = uow;
    }

    public string GenerateRefreshToken(Teacher teacher)
    {
        return generateToken(teacher, _jwtConfigProperties.RefreshKeyBytes, _jwtConfigProperties.RefreshExpirationInSeconds);
    }

    public string GenerateToken(Teacher teacher)
    {
        return generateToken(teacher, _jwtConfigProperties.AccessKeyBytes, _jwtConfigProperties.AccessExpirationInSeconds);
    }

    public string GetEmailFromRefreshToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = _jwtConfigProperties.GetTokenValidationParameters();
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(_jwtConfigProperties.RefreshKeyBytes);
            var tokenObject = tokenHandler.ValidateToken(token, validationParameters, out _);
            return tokenObject.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
        }
        catch (SecurityTokenExpiredException)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            throw JwtExpiredException.WithDateTimes(jwt.ValidFrom, jwt.ValidTo);
        }
        catch (SecurityTokenSignatureKeyNotFoundException)
        {
            throw new JwtIncorrectSignatureKey();
        }
        catch (ArgumentException)
        {
            throw new JwtInvalidTokenException();
        }
    }

    public void InvalidateToken(string token)
    {
        _uow.BeginTransaction();
        if (_uow.InvalidatedTokens.Any(invalidatedToken => invalidatedToken.Token == token))
        {
            return;
        }
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
        _uow.InvalidatedTokens.Add(new InvalidatedToken
        {
            Token = token,
            ExpiresAt = jwt.ValidTo,
        });
        _uow.Commit();
    }

    public void ValidateToken(string token)
    {
        if (_uow.InvalidatedTokens.Any(invalidatedToken => invalidatedToken.Token == token))
        {
            throw new JwtInvalidTokenException();
        }
    }

    private string generateToken(Teacher teacher, byte[] key, int expirationInSeconds)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, teacher.Id.ToString()),
                new Claim(ClaimTypes.Email, teacher.Email),
            }),
            Expires = DateTime.UtcNow.AddSeconds(expirationInSeconds),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}