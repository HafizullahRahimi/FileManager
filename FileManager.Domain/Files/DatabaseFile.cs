using FileManager.Domain.ProfileImages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileManager.Domain.Files;
public class DatabaseFile : BaseFile
{
    [Required]
    [Column(TypeName = "varbinary(max)")]
    public byte[] Data { get; set; } = Array.Empty<byte>();

    public DatabaseFile()
    {
        StorageType = FileStorageType.Database;
    }

    public virtual ProfileImage? ProfileImage { get; set; } // Optional
}