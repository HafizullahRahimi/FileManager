using FileManager.Domain.Base;

namespace FileManager.Domain.Files.Base;
public abstract class BaseFile : FullAuditedEntity<Guid>
{
    public string Name { get; set; } = string.Empty; // مثلا invoice.pdf
    public string ContentType { get; set; } = string.Empty; // مثل image/png, application/pdf
    public long SizeInBytes { get; set; }


    public FileStorageType StorageType { get; set; } // Enum: Local, Database, Cloud

    //public string Path { get; set; } = string.Empty; // Local
    //public required byte[] Data { get; set; } //  Database
    //public string Url { get; set; } = string.Empty; // Cloud

}