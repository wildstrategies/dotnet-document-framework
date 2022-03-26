namespace WildStrategies.DocumentFramework
{
    public interface IEntitySerializer<T>
    {
        T Serialize<TEntity>(TEntity entity)
            where TEntity : Entity;

        TEntity Deserialize<TEntity>(T entity)
            where TEntity : Entity;

    }
}
