using HyperProf.Config.Properties;
using Microsoft.Extensions.Options;

namespace HyperProf.Core.Services.Storage;

public class LocalStorageService : IStorageService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public readonly LocalStorageConfigProperties _localStorageConfigProperties;

    public LocalStorageService(
        IHttpContextAccessor httpContextAccessor,
        IOptions<LocalStorageConfigProperties> localStorageConfigProperties)
    {
        _httpContextAccessor = httpContextAccessor;
        _localStorageConfigProperties = localStorageConfigProperties.Value;
    }

    public string UploadFile(string fileName, Stream stream, string _)
    {
        fileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        var filePath = Path.Combine(_localStorageConfigProperties.RootPath, "uploads", fileName);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            stream.CopyTo(fileStream);
        }
        var request = _httpContextAccessor.HttpContext?.Request;
        if (request != null)
        {
            var baseUrl = $"{request.Scheme}://{request.Host}";
            return $"{baseUrl}/uploads/{fileName}";
        }
        return $"uploads/{fileName}";
    }
}