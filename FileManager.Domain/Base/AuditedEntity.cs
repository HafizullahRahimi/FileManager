namespace FileManager.Domain.Base;

public abstract class AuditedEntity<TId> : ModifiedEntity<TId>
    where TId : notnull
{
}