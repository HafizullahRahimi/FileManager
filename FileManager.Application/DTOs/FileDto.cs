namespace FileManager.Application.DTOs
{
    public class FileDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}