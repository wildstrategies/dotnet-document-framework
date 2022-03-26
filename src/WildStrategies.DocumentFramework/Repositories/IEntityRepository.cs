namespace WildStrategies.DocumentFramework
{
    public interface IEntityRepository<T> : IEntityReadOnlyRepository<T> where T : Entity
    {
        Task<T> CreateOrUpdate(T entity);
        Task Delete(T entity);
    }
}
