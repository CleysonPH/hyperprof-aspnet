namespace HyperProf.Core.Models;

public class Teacher : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal HourlyPrice { get; set; }
    public string? ProfilePicture { get; set; }
    public string Password { get; set; } = string.Empty;

    public IEnumerable<Student> Students { get; set; } = new List<Student>();
}