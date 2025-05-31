namespace FileManager.Domain.Base;

public interface IModifiedEntity
{
    DateTime ModifiedUtcDate { get; set; }
    string ModifiedBy { get; set; }
}