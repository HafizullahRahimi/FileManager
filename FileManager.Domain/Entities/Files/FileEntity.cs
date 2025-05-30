namespace FileManager.Domain.Entities.Files;
public abstract class FileEntity
{
    public Guid Id { get; set; }
    public DateTime UploadedAt { get; set; }
    public string UploadedBy { get; set; } = string.Empty;


    public string Name { get; set; } = string.Empty; // مثلا invoice.pdf
    public string ContentType { get; set; }      // مثل image/png, application/pdf
    public long SizeInBytes { get; set; }


    public FileStorageType StorageType { get; set; } // Enum: Local, Database, Azure, etc.
    public string? PathOrUrl { get; set; } // مسیر یا URL فایل (بسته به StorageType)

}