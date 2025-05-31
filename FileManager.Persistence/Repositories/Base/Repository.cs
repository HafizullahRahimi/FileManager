﻿using FileManager.Domain.Base;
using FileManager.Domain.Base.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Persistence.Repositories.Base;
public class Repository<TEntity> : RepositoryWithFilter<TEntity>, IRepository<TEntity>
    where TEntity : CreatedEntity<Guid>, new()
{
    protected Repository(IDbContextFactory<ApplicationDbContext> dbcontextFactory) : base(dbcontextFactory) { }

    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<TEntity>()
            .OrderByDescending(e => e.CreatedUtcDate)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbContext.Set<TEntity>()
            .SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        await SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        DbContext.Set<TEntity>().Update(entity);
        //DbContext.Entry(entity).State = EntityState.Modified; 
        await SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        DbContext.Set<TEntity>().Remove(entity);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
            DbContext.Set<TEntity>().Remove(entity);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        return entity != null;
    }
    public async Task DeleteAsync(ICollection<TEntity> entities, CancellationToken cancellationToken)
    {
        DbContext.Set<TEntity>().RemoveRange(entities);
        await SaveChangesAsync(cancellationToken);
    }

    private async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}