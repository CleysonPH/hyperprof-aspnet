namespace HyperProf.Api.Students.Dtos;

public class StudentRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DataAula { get; set; }
}