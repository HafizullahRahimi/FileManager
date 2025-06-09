using FileManager.Domain.Files;

namespace FileManager.Domain.Services.Infrastructure.Storage.Models;
public class FileUploadRequest
{
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public Stream FileStream { get; set; } = Stream.Null;
    //public FileStorageType StorageType { get; set; }
}