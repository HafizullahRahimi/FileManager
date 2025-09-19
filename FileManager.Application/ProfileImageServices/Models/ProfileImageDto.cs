namespace FileManager.Application.ProfileImageServices.Models;
public class ProfileImageDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public byte[] Data { get; set; } = Array.Empty<byte>();
    public string Url { get; set; } = string.Empty;
    public DateTime CreatedUtcDate { get; set; }
}