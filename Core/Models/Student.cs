namespace HyperProf.Core.Models;

public class Student : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime ClassDate { get; set; }

    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;
}