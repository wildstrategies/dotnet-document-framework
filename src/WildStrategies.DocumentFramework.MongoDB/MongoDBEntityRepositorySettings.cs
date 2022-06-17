using System.ComponentModel.DataAnnotations;

namespace WildStrategies.DocumentFramework
{
    public class MongoDBEntityRepositoryBaseSettings
    {
        [Required] public string ConnectionString { get; init; } = null!;
        [Required] public bool AllowInsecureTls { get; init; } = false;
    }

    public class MongoDBEntityRepositorySettings : MongoDBEntityRepositoryBaseSettings
    {
        public MongoDBEntityRepositorySettings() { }
        [Required] public string DatabaseName { get; init; } = null!;
        [Required] public string CollectionName { get; init; } = null!;
    }
}
