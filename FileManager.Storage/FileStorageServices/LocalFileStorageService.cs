using FileManager.Domain.Services.Infrastructure;
using Microsoft.Extensions.Hosting;

namespace FileManager.Storage
{
    public class LocalFileStorageService : IFileStorageService
    {
        //Storage/ - Root directory for all storage
        //Storage/Uploads/ - For active uploaded files
            //Documents/ - For document files(.pdf, .doc, etc.)
            //Images/ - For image files(.jpg, .png, etc.)
            //Media/ - For media files(.mp4, .mp3, etc.)
        //Storage/Temp/ - For temporary file operations
        //Storage/Archive/ - For soft-deleted files


        private readonly string _basePath;
        private const string UPLOADS_DIR = "Uploads";
        private const string TEMP_DIR = "Temp";
        private const string ARCHIVE_DIR = "Archive";

        public LocalFileStorageService(IHostEnvironment env)
        {
            // Initialize base storage path
            _basePath = Path.Combine(env.ContentRootPath, "Storage");

            // Ensure all required directories exist
            EnsureDirectoryStructure();
        }

        public async Task<string> UploadAsync(Stream fileStream, string fileName)
        {
            // Determine the appropriate subdirectory based on file type
            string fileType = DetermineFileType(fileName);
            string targetDir = Path.Combine(_basePath, UPLOADS_DIR, fileType);

            // Generate a unique filename to prevent collisions
            string uniqueFileName = GenerateUniqueFileName(fileName);
            var path = Path.Combine(targetDir, uniqueFileName);

            using var fs = new FileStream(path, FileMode.Create);
            await fileStream.CopyToAsync(fs);
            return path;
        }

        public async Task<byte[]> DownloadAsync(string storedPath)
        {
            if (!File.Exists(storedPath))
                throw new FileNotFoundException("The requested file was not found.", storedPath);

            return await File.ReadAllBytesAsync(storedPath);
        }

        public Task<bool> DeleteAsync(string storedPath)
        {
            try
            {
                if (File.Exists(storedPath))
                {
                    // Move to archive instead of permanent deletion
                    string fileName = Path.GetFileName(storedPath);
                    string archivePath = Path.Combine(_basePath, ARCHIVE_DIR, fileName);
                    File.Move(storedPath, archivePath, true);
                }
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        private void EnsureDirectoryStructure()
        {
            var directories = new[]
            {
                _basePath,
                Path.Combine(_basePath, UPLOADS_DIR),
                Path.Combine(_basePath, TEMP_DIR),
                Path.Combine(_basePath, ARCHIVE_DIR),
                // Add subdirectories for different file types
                Path.Combine(_basePath, UPLOADS_DIR, "Documents"),
                Path.Combine(_basePath, UPLOADS_DIR, "Images"),
                Path.Combine(_basePath, UPLOADS_DIR, "Media")
            };

            foreach (var dir in directories)
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
            }
        }

        private string DetermineFileType(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".pdf" or ".doc" or ".docx" or ".txt" => "documents",
                ".jpg" or ".jpeg" or ".png" or ".gif" => "images",
                ".mp4" or ".mp3" or ".wav" or ".avi" => "media",
                _ => "other"
            };
        }

        private string GenerateUniqueFileName(string originalFileName)
        {
            string extension = Path.GetExtension(originalFileName);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(originalFileName);
            return $"{fileNameWithoutExt}_{DateTime.UtcNow:yyyyMMdd_HHmmss}_{Guid.NewGuid():N}{extension}";
        }
    }
}