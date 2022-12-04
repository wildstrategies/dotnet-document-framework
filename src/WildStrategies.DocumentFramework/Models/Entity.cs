
using System.ComponentModel.DataAnnotations;

namespace WildStrategies.DocumentFramework
{
    public abstract class Entity : ValueObject, ILastUpdateTimeProvider, IEntity
    {
        [Required] public DateTime LastUpdateTime { get; init; } = DateTime.UtcNow;
    }
}
