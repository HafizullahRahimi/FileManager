using FileManager.Domain.Services.Infrastructure.Storage;
using Microsoft.AspNetCore.StaticFiles;
using FileSignatures;

namespace FileManager.Storage.Services;
public class FileTypeService : IFileTypeService
{
    private readonly FileExtensionContentTypeProvider extensionProvider;
    private readonly FileFormatInspector inspector;

    public FileTypeService()
    {
        extensionProvider = new FileExtensionContentTypeProvider();
        inspector = new FileFormatInspector();
    }

    public string GetContentType(string fileName)
    {
        if (extensionProvider.TryGetContentType(fileName, out var contentType))
            return contentType;

        return "application/octet-stream";
    }

    public string GetContentType(byte[] fileBytes)
    {
        using var stream = new MemoryStream(fileBytes);
        return GetContentType(stream);
    }

    public string GetContentType(Stream stream)
    {
        var format = inspector.DetermineFileFormat(stream);
        return format?.MediaType ?? "application/octet-stream";
    }
}