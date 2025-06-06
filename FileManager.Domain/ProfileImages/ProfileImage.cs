using FileManager.Domain.Base;
using FileManager.Domain.Files;

namespace FileManager.Domain.ProfileImages;
public class ProfileImage : FullAuditedEntity<Guid>
{
    public string UserName { get; set; } = string.Empty;
    public Guid DatabaseFileId { get; set; }
    public DatabaseFile DatabaseFile { get; set; } = null!; // required
}