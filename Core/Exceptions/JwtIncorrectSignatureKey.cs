namespace HyperProf.Core.Exceptions;

public class JwtIncorrectSignatureKey : JwtException
{
    public JwtIncorrectSignatureKey(string message = "Chave de assinatura do token está incorreta") : base(message)
    { }
}