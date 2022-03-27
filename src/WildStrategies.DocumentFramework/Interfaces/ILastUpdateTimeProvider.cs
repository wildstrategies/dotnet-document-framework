using System;

namespace WildStrategies.DocumentFramework
{
    public interface ILastUpdateTimeProvider
    {
        DateTime LastUpdateTime { get; }
    }
}
