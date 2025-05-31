namespace FileManager.Domain.Files.Base;
public class LocalFile : BaseFile
{
    public string Path { get; set; } = string.Empty;

    public LocalFile()
    {
        StorageType = FileStorageType.Local;
    }
}