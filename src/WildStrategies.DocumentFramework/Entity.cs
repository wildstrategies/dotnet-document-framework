using NodaTime;

namespace WildStrategies.DocumentFramework
{
    public abstract class Entity : ValueObject, ILatsUpdateTimeProvider
    {
        public Instant LatsUpdateTime { get; protected set; } = SystemClock.Instance.GetCurrentInstant();
    }
}
