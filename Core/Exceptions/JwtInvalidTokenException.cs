namespace HyperProf.Core.Exceptions;

public class JwtInvalidTokenException : JwtException
{
    public JwtInvalidTokenException(string message = "Token inválido") : base(message)
    { }
}