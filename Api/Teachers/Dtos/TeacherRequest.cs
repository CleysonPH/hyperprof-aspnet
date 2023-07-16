using System.Text.Json.Serialization;

namespace HyperProf.Api.Teachers.Dtos;

public class TeacherRequest
{
    [JsonIgnore]
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Idade { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal ValorHora { get; set; }
    public string Password { get; set; } = string.Empty;
    public string PasswordConfirmation { get; set; } = string.Empty;
}