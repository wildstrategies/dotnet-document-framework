using System;

namespace WildStrategies.DocumentFramework
{
    public interface ICreatedTimeProvider
    {
        DateTime CreatedTime { get; }
    }
}
