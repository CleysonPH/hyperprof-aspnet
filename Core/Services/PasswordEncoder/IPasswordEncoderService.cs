namespace HyperProf.Core.Services.PasswordEncoder;

public interface IPasswordEncoderService
{
    string Encode(string password);
    bool Verify(string password, string hash);
}