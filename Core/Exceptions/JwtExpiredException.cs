using System.IdentityModel.Tokens.Jwt;

namespace HyperProf.Core.Exceptions;

public class JwtExpiredException : JwtException
{
    public JwtExpiredException(string message) : base(message)
    { }

    public static JwtExpiredException WithDateTime(DateTime expiresAt)
    {
        return new JwtExpiredException($"Token expirou em {expiresAt.ToString("o")}");
    }

    public static JwtExpiredException WithDateTimes(DateTime issuedAt, DateTime expiresAt)
    {
        return new JwtExpiredException($"Token expirou em {expiresAt.ToString("o")} e foi emitido em {issuedAt.ToString("o")}");
    }

    public static JwtExpiredException WithToken(string token)
    {
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
        return new JwtExpiredException($"Token expirou em {jwt.ValidTo.ToString("o")} e foi emitido em {jwt.ValidFrom.ToString("o")}");
    }
}