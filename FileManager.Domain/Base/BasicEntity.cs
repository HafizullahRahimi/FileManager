namespace FileManager.Domain.Base;

public abstract class BasicEntity<TId> : BaseEntity<TId>
    where TId : notnull
{
}