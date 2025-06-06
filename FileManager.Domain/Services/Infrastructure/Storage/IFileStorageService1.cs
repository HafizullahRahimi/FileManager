namespace FileManager.Domain.Services.Infrastructure.Storage
{
    public interface IFileStorageService1
    {
        Task<string> UploadAsync(Stream fileStream, string fileName);
        Task<byte[]> DownloadAsync(string storedPath);
        Task<bool> DeleteAsync(string storedPath);
    }
}