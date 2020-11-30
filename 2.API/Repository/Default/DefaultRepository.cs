using System;
using System.Linq;
using _2.API.Infra;
using _2.API.Models;
using Microsoft.EntityFrameworkCore;

namespace _2.API.Repository.Default
{
    internal abstract class DefaultRepository<TEntity> : IDefaultRepository<TEntity>
        where TEntity : class, IHaveId
    {
        private readonly DbContext _dbContext;

        protected DefaultRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext.Instance;
        }

        public virtual TEntity Find(Guid key)
        {
            return DbSet
                .FirstOrDefault(x => x.Id == key);
        }

        public virtual void Store(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            var entry = _dbContext.ChangeTracker.Entries().FirstOrDefault(x => (Guid)x.Property("Id").CurrentValue == entity.Id);

            if (entry != null)
            {
                entry.State = EntityState.Detached; // Caso já tenha sido adicionado ao contexto, remove a entidade para poder adicionar a nova versão
            }

            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Remove(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        protected virtual IQueryable<TEntity> DbSet => _dbContext.Set<TEntity>().AsNoTrackingWithIdentityResolution();
    }
}