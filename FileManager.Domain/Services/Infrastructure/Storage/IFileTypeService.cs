namespace FileManager.Domain.Services.Infrastructure.Storage;
public interface IFileTypeService
{
    string GetContentType(string fileName);
    string GetContentType(byte[] fileBytes);
    string GetContentType(Stream stream);
}