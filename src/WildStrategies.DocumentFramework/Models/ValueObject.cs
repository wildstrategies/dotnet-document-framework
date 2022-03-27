using System.ComponentModel.DataAnnotations;

namespace WildStrategies.DocumentFramework
{
    public abstract class ValueObject : DocumentFrameworkObject, IIDProvider, ICreatedTimeProvider, IValueObject
    {
        [Required] public Guid Id { get; protected set; } = Guid.NewGuid();
        [Required] public DateTime CreatedTime { get; protected set; } = DateTime.UtcNow;
    }
}
