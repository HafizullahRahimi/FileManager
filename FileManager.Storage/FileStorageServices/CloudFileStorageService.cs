using FileManager.Domain.Files;
using FileManager.Domain.Services.Infrastructure.Storage;

namespace FileManager.Storage.FileStorageServices;
public class CloudFileStorageService : ICloudFileStorageService
{
    public Task<CloudFile> UploadAsync(FileUploadRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    public Task<CloudFile?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    public Task DeleteAsync(CloudFile file, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}