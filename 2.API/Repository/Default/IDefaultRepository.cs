namespace _2.API.Repository.Default
{
    public interface IDefaultRepository<TEntity> : ICrudRepository<TEntity> where TEntity : class
    {
    }
}