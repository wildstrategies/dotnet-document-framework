using NodaTime;

namespace WildStrategies.DocumentFramework
{
    public interface IDeletedProvider
    {
        Instant DeletedTime { get; }
        bool Deleted { get; }
    }
}
