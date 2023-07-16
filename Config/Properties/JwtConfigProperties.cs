using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace HyperProf.Config.Properties;

public class JwtConfigProperties
{
    public string AccessKey { get; set; } = string.Empty;
    public string RefreshKey { get; set; } = string.Empty;
    public int AccessExpirationInSeconds { get; set; }
    public int RefreshExpirationInSeconds { get; set; }

    public byte[] AccessKeyBytes => Encoding.UTF8.GetBytes(AccessKey);
    public byte[] RefreshKeyBytes => Encoding.UTF8.GetBytes(RefreshKey);

    public TokenValidationParameters GetTokenValidationParameters()
    {
        return new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(AccessKeyBytes)
        };
    }
}