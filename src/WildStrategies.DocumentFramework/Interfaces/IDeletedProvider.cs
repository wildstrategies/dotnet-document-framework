namespace WildStrategies.DocumentFramework
{
    public interface IDeletedProvider
    {
        DateTime? DeletedTime { get; }
        bool Deleted { get; }
    }
}
