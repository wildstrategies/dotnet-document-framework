
namespace WildStrategies.DocumentFramework
{
    public abstract class Entity : ValueObject, ILatsUpdateTimeProvider, IEntity
    {
        public DateTime LatsUpdateTime { get; protected set; } = DateTime.UtcNow;
    }
}
