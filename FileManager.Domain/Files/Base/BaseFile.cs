namespace FileManager.Domain.Files.Base;
public abstract class BaseFile
{
    public Guid Id { get; set; }
    public DateTime UploadedAt { get; set; }
    public string UploadedBy { get; set; } = string.Empty;


    public string Name { get; set; } = string.Empty; // مثلا invoice.pdf
    public string ContentType { get; set; } = string.Empty; // مثل image/png, application/pdf
    public long SizeInBytes { get; set; }


    public FileStorageType StorageType { get; set; } // Enum: Local, Database, Cloud

    //public string Path { get; set; } = string.Empty; // Local
    //public required byte[] Data { get; set; } //  Database
    //public string Url { get; set; } = string.Empty; // Cloud

}