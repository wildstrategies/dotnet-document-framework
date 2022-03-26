namespace WildStrategies.DocumentFramework
{
    public abstract class ValueObject : DocumentFrameworkObject, IIDProvider, ICreatedTimeProvider, IValueObject
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public DateTime CreatedTime { get; protected set; } = DateTime.UtcNow;
    }
}
