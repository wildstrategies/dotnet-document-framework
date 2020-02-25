using NodaTime;

namespace WildStrategies.DocumentFramework
{
    public interface ILatsUpdateTimeProvider
    {
        Instant LatsUpdateTime { get; }
    }
}
