namespace HyperProf.Api.Teachers.Dtos;

public class TeacherResponse
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Idade { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal ValorHora { get; set; }
    public string? FotoPerfil { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}