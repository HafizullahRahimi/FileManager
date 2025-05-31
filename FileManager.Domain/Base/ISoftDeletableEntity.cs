namespace FileManager.Domain.Base;

public interface ISoftDeletableEntity
{
    bool IsDeleted { get; set; }
    //DateTime? DeletedUtcDate { get; set; }
    //string? DeletedBy { get; set; }
}