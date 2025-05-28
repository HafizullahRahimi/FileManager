using FileManager.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace FileManager.Infrastructure.FileSystem
{
    public class PhysicalFileStorageService : IFileStorageService
    {
        private readonly string _basePath;

        public PhysicalFileStorageService(IWebHostEnvironment env)
        {
            _basePath = Path.Combine(env.ContentRootPath, "App_Data", "Uploads");
            if (!Directory.Exists(_basePath))
                Directory.CreateDirectory(_basePath);
        }

        public async Task<string> UploadAsync(Stream fileStream, string fileName)
        {
            var path = Path.Combine(_basePath, fileName);
            using var fs = new FileStream(path, FileMode.Create);
            await fileStream.CopyToAsync(fs);
            return path;
        }

        public async Task<byte[]> DownloadAsync(string storedPath)
        {
            return await File.ReadAllBytesAsync(storedPath);
        }

        public Task<bool> DeleteAsync(string storedPath)
        {
            if (File.Exists(storedPath)) File.Delete(storedPath);
            return Task.FromResult(true);
        }
    }
}