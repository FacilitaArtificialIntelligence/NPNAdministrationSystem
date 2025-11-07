namespace AdministrationSystem.Application.Common.Interfaces;

public interface ICloudStorageService
{
    Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType);
}
