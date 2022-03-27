
namespace WildStrategies.DocumentFramework
{
    public interface IValueObject
    {
        DateTime CreatedTime { get; }
        Guid Id { get; }
    }
}