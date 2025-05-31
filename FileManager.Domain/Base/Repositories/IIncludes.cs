﻿namespace FileManager.Domain.Base.Repositories;
public interface IIncludes<TEntity>
{
    IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query);
}