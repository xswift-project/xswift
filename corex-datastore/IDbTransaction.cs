﻿using Microsoft.EntityFrameworkCore.Storage;

namespace CoreX.Datastore
{
    public delegate void DbUpdateConcurrencyConflictOccurred();

    public interface IDbTransaction
    {
        Task<IDbContextTransaction> BeginAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task<int> SaveChangesAsync(
            bool concurrencyCheck = false,
            DbUpdateConcurrencyConflictOccurred? toCheckConcurrencyConflictOccurred = null);
    }
}