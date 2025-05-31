using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileManager.Domain.Files.Base;
public class DatabaseFile : BaseFile
{
    [Required]
    [Column(TypeName = "varbinary(max)")]
    public byte[] Data { get; set; } = Array.Empty<byte>();

    public DatabaseFile()
    {
        StorageType = FileStorageType.Database;
    }
}