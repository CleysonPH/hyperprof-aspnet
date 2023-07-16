namespace HyperProf.Core.Exceptions;

public class UnauthenticatedException : AuthenticationException
{
    public UnauthenticatedException(string message = "Não autenticado") : base(message)
    { }
}