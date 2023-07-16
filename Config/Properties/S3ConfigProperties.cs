using Amazon;

namespace HyperProf.Config.Properties;

public class S3ConfigProperties
{
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string BucketName { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;

    public RegionEndpoint GetRegionEndpoint()
    {
        return RegionEndpoint.GetBySystemName(Region);
    }
}