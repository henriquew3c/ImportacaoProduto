using System;

namespace _2.API.Repository.Default
{
    public interface ICrudRepository<TEntity> where TEntity : class
    {
        TEntity Find(Guid key);
        void Store(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}