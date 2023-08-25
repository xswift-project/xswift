﻿using MediatR;

namespace CoreX.Domain
{
    public abstract class DeletionRequest<TRequest, TEntity> :
        CommandRequest<TEntity>, IRequestResolver<TRequest, TEntity>
        where TRequest : Request where TEntity : BaseEntity
    {
        public virtual TEntity ResolveAndGetEntity()
        {
            throw new NotImplementedException();
        }
        public virtual List<TEntity> ResolveAndGetEntities()
        {
            throw new NotImplementedException();
        }
        public virtual Task<TEntity> ResolveAndGetEntityAsync(IMediator mediator)
        {
            throw new NotImplementedException();
        }
        public virtual Task<List<TEntity>> ResolveAndGetEntitiesAsync(IMediator mediator)
        {
            throw new NotImplementedException();
        }
        public virtual Task ResolveAsync(IMediator mediator)
        {
            throw new NotImplementedException();
        }
        public virtual void Resolve(TEntity entity)
        {
            entity.Delete();
        }
        public virtual void Resolve(List<TEntity> entities)
        {
            entities.ForEach(e => { e.Delete(); });
        }
        public async virtual Task ResolveAsync(IMediator mediator, TEntity entity)
        {
            entity.Delete();
        }
        public async virtual Task ResolveAsync(IMediator mediator, List<TEntity> entities)
        {
            entities.ForEach(async entity => {
                entity.Delete();
            });
        }
    }
}
