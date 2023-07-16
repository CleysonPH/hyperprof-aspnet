namespace HyperProf.Core.Services.PasswordEncoder;

public class BCryptPasswordEncoderService : IPasswordEncoderService
{
    public string Encode(string password)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        return BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

    public bool Verify(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}