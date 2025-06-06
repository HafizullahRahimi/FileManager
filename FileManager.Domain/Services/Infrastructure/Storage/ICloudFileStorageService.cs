using FileManager.Domain.Files;

namespace FileManager.Domain.Services.Infrastructure.Storage;
public interface ICloudFileStorageService : IFileStorageService<CloudFile>
{
}