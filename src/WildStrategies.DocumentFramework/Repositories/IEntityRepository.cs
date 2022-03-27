namespace WildStrategies.DocumentFramework
{
    public interface IEntityRepository<T> : IEntityReadOnlyRepository<T> where T : Entity
    {
        Task UpdateAsync(T entity);
        Task InsertAsync(T entity);
        Task InsertManyAsync(IEnumerable<T> entities);
        Task DeleteAsync(T entity);
    }
}
