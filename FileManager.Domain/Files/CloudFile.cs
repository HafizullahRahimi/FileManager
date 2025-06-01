namespace FileManager.Domain.Files;
public class CloudFile : BaseFile
{
    public string Url { get; set; } = string.Empty;

    public CloudFile()
    {
        StorageType = FileStorageType.Cloud;
    }
}