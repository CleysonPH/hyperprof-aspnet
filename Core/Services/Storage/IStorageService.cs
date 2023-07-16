namespace HyperProf.Core.Services.Storage;

public interface IStorageService
{
    string UploadFile(string fileName, Stream stream, string contentType);
}