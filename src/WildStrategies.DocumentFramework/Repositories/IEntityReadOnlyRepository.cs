namespace WildStrategies.DocumentFramework
{
    public interface IEntityReadOnlyRepository<T> : IAsyncDisposable, IDisposable where T : IEntity
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T?> GetAsync(Guid id);
        IQueryable<T> AsQueryable();
    }
}
