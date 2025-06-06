using FileManager.Domain.Files;

namespace FileManager.Domain.Services.Infrastructure.Storage;
public class FileUploadRequest
{
    public string Name { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public Stream Content { get; set; } = Stream.Null;
    //public FileStorageType StorageType { get; set; }
}