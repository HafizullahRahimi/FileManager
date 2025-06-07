namespace FileManager.Domain.Services.Infrastructure.Storage.Models;
public class FileData
{
    public byte[] Content { get; }
    public string FileName { get; }

    public FileData(byte[] content, string fileName)
    {
        Content = content;
        FileName = fileName;
    }
}