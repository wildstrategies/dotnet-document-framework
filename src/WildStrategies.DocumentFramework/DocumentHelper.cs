namespace WildStrategies.DocumentFramework
{
    public static class DocumentHelper
    {
        public static Document<TEntity> CreateDocument<TEntity>(this TEntity root) where TEntity : Entity
        {
            return new Document<TEntity>(root);
        }
    }
}
