using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using AdministrationSystem.Application.Common.Interfaces;

namespace AdministrationSystem.Infrastructure.Common.Storage;
public class GoogleCloudStorageService : ICloudStorageService
{
    private readonly string _bucketName;
    private readonly StorageClient _storageClient;

    public GoogleCloudStorageService(IConfiguration configuration)
    {
        _bucketName = configuration["GoogleCloud:BucketName"]!;
        _storageClient = StorageClient.Create();
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
    {
        var objectName = $"profile-pictures/{Guid.NewGuid()}_{fileName}";
        var obj = await _storageClient.UploadObjectAsync(_bucketName, objectName, contentType, fileStream);
        return $"https://storage.googleapis.com/{_bucketName}/{objectName}";
    }
}
