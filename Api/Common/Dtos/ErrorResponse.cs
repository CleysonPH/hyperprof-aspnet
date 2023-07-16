namespace HyperProf.Api.Common.Dtos;

public class ErrorResponse
{
    public string Message { get; set; } = string.Empty;
    public int Status { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Cause { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }

    public ErrorResponse()
    {
        Timestamp = DateTime.UtcNow;
    }
}