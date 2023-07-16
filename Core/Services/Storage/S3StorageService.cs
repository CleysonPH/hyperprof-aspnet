using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using HyperProf.Config.Properties;
using Microsoft.Extensions.Options;

namespace HyperProf.Core.Services.Storage;

public class S3StorageService : IStorageService
{
    private readonly S3ConfigProperties _s3ConfigProperties;
    private readonly TransferUtility _transferUtility;

    public S3StorageService(IOptions<S3ConfigProperties> s3ConfigProperties)
    {
        _s3ConfigProperties = s3ConfigProperties.Value;
        _transferUtility = createTransferUtility();
    }

    public string UploadFile(string fileName, Stream stream, string contentType)
    {
        var transferUtilityUploadRequest = new TransferUtilityUploadRequest
        {
            InputStream = stream,
            Key = $"{Guid.NewGuid()}-{Path.GetExtension(fileName)}",
            BucketName = _s3ConfigProperties.BucketName,
            CannedACL = S3CannedACL.PublicRead,
            ContentType = contentType
        };
        _transferUtility.Upload(transferUtilityUploadRequest);
        return $"https://{_s3ConfigProperties.BucketName}.s3.{_s3ConfigProperties.Region}.amazonaws.com/{transferUtilityUploadRequest.Key}";
    }

    private TransferUtility createTransferUtility()
    {
        var credentials = new BasicAWSCredentials(_s3ConfigProperties.AccessKey, _s3ConfigProperties.SecretKey);
        var s3Config = new AmazonS3Config
        {
            RegionEndpoint = _s3ConfigProperties.GetRegionEndpoint()
        };
        var client = new AmazonS3Client(credentials, s3Config);
        return new TransferUtility(client);
    }
}