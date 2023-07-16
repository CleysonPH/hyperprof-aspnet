namespace HyperProf.Core.Exceptions;

public class TeacherNotFoundException : ModelNotFoundException
{
    public TeacherNotFoundException(string message) : base(message)
    { }

    public static TeacherNotFoundException WithId(int id)
    {
        return new TeacherNotFoundException($"Professor com id {id} não encontrado");
    }
}