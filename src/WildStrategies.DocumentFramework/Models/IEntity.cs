
namespace WildStrategies.DocumentFramework
{
    public interface IEntity : IValueObject
    {
        DateTime LastUpdateTime { get; }
    }
}