using NodaTime;

namespace WildStrategies.DocumentFramework
{
    public interface ILatsUpdateTime
    {
        Instant LatsUpdateTime { get; }
    }
}
