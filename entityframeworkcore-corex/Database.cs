﻿using CoreX.Base;
using CoreX.Domain;
using Microsoft.EntityFrameworkCore;
using SoftDeleteServices.Concrete;
using SoftDeleteServices.Configuration;
using System.Linq.Expressions;

namespace EntityFrameworkCore.CoreX
{
    public abstract class Database : IDatabase
    {
        private bool _trackingMode = true;
        public bool TrackingMode { get { return _trackingMode; } }
        private readonly DbContext _context;

        public Database(DbContext context)
        {
            _context = context;
        }

        public TDbContext GetDbContext<TDbContext>() where TDbContext : DbContext
        {
            return (TDbContext)_context;
        }

        public void AsNoTraking()
        {
            _trackingMode = false;
        }

        #region create Update
        public async Task CreateAsync<TEntity>(
            TEntity entity,
            Expression<Func<TEntity, bool>>? uniqueSpecifications = null)
            where TEntity : BaseEntity
        {
            var _dbSet = _context.Set<TEntity>();

            if (uniqueSpecifications != null)
                await _context.ThrowIfTheEntityWithTheIdentificationsAlreadyExists(uniqueSpecifications);

            _dbSet.Add(entity);
        }

        public async Task UpdateAsync<TEntity>(
            object KeyValue,
            Expression<Func<TEntity, bool>>? uniqueSpecifications = null)
            where TEntity : BaseEntity
        {
            await UpdateAsync(uniqueSpecifications, KeyValues: KeyValue);
        }
        public async Task UpdateAsync<TEntity>(
            Expression<Func<TEntity, bool>>? uniqueSpecifications = null,
            params object[] KeyValues)
            where TEntity : BaseEntity
        {
            var identifierCondition = LinqHelper.KeyValuesToExpression<TEntity>(keyValues: KeyValues);
            var whereBuilder = new ExpressionBuilder<TEntity>();
            whereBuilder.And(identifierCondition);
            whereBuilder.And(uniqueSpecifications!);
            var expression = whereBuilder.GetExpression();

            if (uniqueSpecifications != null)
                await _context.ThrowIfTheEntityWithTheIdentificationsAlreadyExists(expression);
        }
        #endregion

        #region delete
        public async Task DeleteAsync<TEntity>(
            TEntity entity,
            CascadeSoftDeleteConfiguration<ISoftDelete> configCascadeDelete) 
            where TEntity : BaseEntity
        {
            var service = new CascadeSoftDelServiceAsync<ISoftDelete>(configCascadeDelete);
            await service.SetCascadeSoftDeleteAsync(entity, callSaveChanges: false);
        }

        public async Task UndoDeletingAsync<TEntity>(
            TEntity entity,
            object KeyValue,
            CascadeSoftDeleteConfiguration<ISoftDelete> configCascadeDelete,
            Expression<Func<TEntity, bool>>? uniqueSpecifications = null) 
            where TEntity : BaseEntity
        {
            await UndoDeletingAsync(entity, configCascadeDelete,
                uniqueSpecifications, KeyValues: KeyValue);
        }
        public async Task UndoDeletingAsync<TEntity>(
            TEntity entity,
            CascadeSoftDeleteConfiguration<ISoftDelete> configCascadeDelete,
            Expression<Func<TEntity, bool>>? uniqueSpecifications = null,
            params object[] KeyValues)
            where TEntity : BaseEntity
        {
            var identifierCondition = LinqHelper.KeyValuesToExpression<TEntity>(keyValues: KeyValues);
            var whereBuilder = new ExpressionBuilder<TEntity>();
            whereBuilder.And(identifierCondition);
            whereBuilder.And(uniqueSpecifications!);
            var expression = whereBuilder.GetExpression();
            if (expression != null)
                await _context.ThrowIfTheEntityWithTheIdentificationsAlreadyExists(expression);

            var service = new CascadeSoftDelServiceAsync<ISoftDelete>(configCascadeDelete);
            await service.ResetCascadeSoftDeleteAsync(entity, callSaveChanges: false);
        }
        public async Task<bool> IsValidToDeletePhysically<TEntity>(
             TEntity entity,
            CascadeSoftDeleteConfiguration<ISoftDelete> configCascadeDelete)
            where TEntity : BaseEntity
        {
            var service = new CascadeSoftDelServiceAsync<ISoftDelete>(configCascadeDelete);
            return (await service.CheckCascadeSoftDeleteAsync(entity)).IsValid;
        }

        public async Task DeletePhysically<TEntity>(TEntity entity) 
            where TEntity : BaseEntity
        {

            var _dbSet = _context.Set<TEntity>();
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }
        public async Task DeletePhysicallyAsync<TEntity>(
            Expression<Func<TEntity, bool>> condition)
            where TEntity : BaseEntity
        {
            // if the entity implements ICascadeSoftDelete interface,
            // It muse check it out for validation.

            throw new NotImplementedException();
        }

        #endregion

        #region retrieve
        public async Task<TEntity> SingleAsync<TEntity>(
          Expression<Func<TEntity, bool>> where,
          Expression<Func<TEntity, object>>? include = null,
          bool? trackingMode = null,
          bool alsoTheDeletedOnes = false,
          bool throwExceptionProvidedEntityWasNotFound = true) 
            where TEntity : BaseEntity
        {
            if (trackingMode == null)
                trackingMode = _trackingMode;

            return await _context.SingleAsync(
                where,
                include: include,
                trackingMode: trackingMode,
                alsoTheDeletedOnes: alsoTheDeletedOnes,
                throwExceptionProvidedEntityWasNotFound:
                throwExceptionProvidedEntityWasNotFound);
        }

        public async Task<TEntity> FindAsync<TEntity>(
            bool throwExceptionIfEntityWasNotFound = false,
            params object[] KeyValues
            ) where TEntity : BaseEntity
        {
            return await _context.FindAsync<TEntity>(
                throwExceptionIfEntityWasNotFound:
                throwExceptionIfEntityWasNotFound, KeyValues);
        }

        public async Task<TEntity> FindAsync<TEntity>(
           object KeyValue,
           bool throwExceptionIfEntityWasNotFound = false
           ) where TEntity : BaseEntity
        {
            return await _context.FindAsync<TEntity>(
                throwExceptionIfEntityWasNotFound:
                throwExceptionIfEntityWasNotFound, KeyValues: KeyValue);
        }

        public virtual async Task<IEnumerable<TEntity>> ToListAsync<TEntity>(
         Expression<Func<TEntity, bool>>? condition = null,
         Expression<Func<TEntity, object>>? orderBy = null,
         Expression<Func<TEntity, object>>? orderByDescending = null,
         Expression<Func<TEntity, object>>? include = null,
         bool? trackingMode = null,
         bool alsoTheDeletedOnes = true) where TEntity : BaseEntity
        {
            if (trackingMode == null)
                trackingMode = _trackingMode;

            return await _context.ToListAsync(
                condition: condition,
                orderBy: orderBy,
                orderByDescending: orderByDescending,
                include: include,
                trackingMode: trackingMode,
                alsoTheDeletedOnes: alsoTheDeletedOnes);
        }
        #endregion
    }
}