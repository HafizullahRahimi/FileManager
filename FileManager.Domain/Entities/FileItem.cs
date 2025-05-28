namespace FileManager.Domain.Entities
{
    public class FileItem
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string StoredPath { get; set; }
        public string UploadedBy { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}