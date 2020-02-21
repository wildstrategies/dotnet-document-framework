using NodaTime;

namespace WildStrategies.DocumentFramework
{
    public interface ICreatedTimeProvider
    {
        Instant CreatedTime { get; }
    }
}
