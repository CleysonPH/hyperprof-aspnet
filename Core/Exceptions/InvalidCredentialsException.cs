namespace HyperProf.Core.Exceptions;

public class InvalidCredentialsException : AuthenticationException
{
    public InvalidCredentialsException() : base("Credenciais inválidas")
    { }
}