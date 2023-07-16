using HyperProf.Core.Models;

namespace HyperProf.Core.Services.Authentication;

public interface IHyperprofAuthenticationService
{
    Teacher Authenticate(string email, string password);
    Teacher Authenticate(string refreshToken);
    Teacher GetAuthenticatedUser();
}