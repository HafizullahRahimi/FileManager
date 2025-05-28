using FileManager.Application.DTOs;
using FileManager.Application.Interfaces;
using FileManager.Domain.Entities;
using FileManager.Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FileManager.Infrastructure.Persistence.Services
{
    public class FileService : IFileService
    {
        private readonly FileDbContext _dbContext;
        private readonly IFileStorageService _storageService;

        public FileService(FileDbContext dbContext, IFileStorageService storageService)
        {
            _dbContext = dbContext;
            _storageService = storageService;
        }

        public async Task<Guid> UploadAsync(Stream fileStream, string fileName, string username)
        {
            try
            {
                //var storedPath = await _storageService.UploadAsync(fileStream, fileName);
                var storedPath = "Filess";
                var file = new FileItem
                {
                    Id = Guid.NewGuid(),
                    FileName = fileName,
                    StoredPath = storedPath,
                    UploadedAt = DateTime.UtcNow,
                    UploadedBy = username
                };
                _dbContext.FileItems.Add(file);
                await _dbContext.SaveChangesAsync();
                return file.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<FileDto>> GetFilesForUserAsync(string username)
        {
            return await _dbContext.FileItems
                .Where(f => f.UploadedBy == username)
                .Select(f => new FileDto
                {
                    Id = f.Id,
                    FileName = f.FileName,
                    UploadedAt = f.UploadedAt
                }).ToListAsync();
        }

        public async Task<byte[]> DownloadAsync(Guid id, string username)
        {
            var file = await _dbContext.FileItems.FirstOrDefaultAsync(f => f.Id == id && f.UploadedBy == username);
            if (file == null) throw new Exception("Unauthorized or file not found.");
            return await _storageService.DownloadAsync(file.StoredPath);
        }
    }
}