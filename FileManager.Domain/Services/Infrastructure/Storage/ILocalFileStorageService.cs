using FileManager.Domain.Files;

namespace FileManager.Domain.Services.Infrastructure.Storage;
public interface ILocalFileStorageService : IFileStorageService<LocalFile>
{
    Task<byte[]> DownloadAsync(string path, CancellationToken cancellationToken);
}