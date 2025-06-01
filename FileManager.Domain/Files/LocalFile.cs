using FileManager.Domain.BlogPostAttachments;
using FileManager.Domain.Files.ProductFiles;

namespace FileManager.Domain.Files;
public class LocalFile : BaseFile
{
    public string Path { get; set; } = string.Empty;

    public LocalFile() // For EF Core
    {
        StorageType = FileStorageType.Local;
    }

    public ProductImage? ProductImage { get; set; } // Optional
    public BlogPostAttachment? BlogPostAttachment { get; set; } // Optional
}