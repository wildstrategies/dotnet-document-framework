using NodaTime;
using System;

namespace WildStrategies.DocumentFramework
{
    public abstract class ValueObject : DocumentFrameworkObject, IIDProvider, ICreatedTimeProvider
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public Instant CreatedTime { get; protected set; } = SystemClock.Instance.GetCurrentInstant();
    }
}
