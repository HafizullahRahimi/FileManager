﻿using FileManager.Domain.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FileManager.Persistence.Interceptors;

public class CreatedInterceptor : BaseInterceptor
{
    public CreatedInterceptor(IHttpContextAccessor httpContextAccessor)
        : base(httpContextAccessor)
    {
    }

    protected override void ProcessEntities(DbContextEventData eventData)
    {
        var entries = eventData.Context!.ChangeTracker
            .Entries<ICreatedEntity>()
            .Where(e => e.State == EntityState.Added);

        foreach (var entry in entries)
        {
            entry.Entity.CreatedBy = CurrentUserId;
            entry.Entity.CreatedUtcDate = DateTime.UtcNow;
        }
    }
}