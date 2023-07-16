namespace HyperProf.Core.Models;

public class InvalidatedToken : BaseModel
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}