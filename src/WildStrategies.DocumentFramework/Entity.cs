using NodaTime;

namespace WildStrategies.DocumentFramework
{
    public abstract class Entity : ValueObject, ILatsUpdateTime
    {
        public Instant LatsUpdateTime { get; protected set; } = SystemClock.Instance.GetCurrentInstant();
    }
}
