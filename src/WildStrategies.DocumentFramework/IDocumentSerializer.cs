namespace WildStrategies.DocumentFramework
{
    public interface IDocumentSerializer<T>
    {
        T Serialize<TEntity>(Document<TEntity> document)
            where TEntity : Entity;

        Document<TEntity> Deserialize<TEntity>(T serialized)
            where TEntity : Entity;

    }
}
