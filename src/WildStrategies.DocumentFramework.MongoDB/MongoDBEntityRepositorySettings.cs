using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildStrategies.DocumentFramework
{
    public class MongoDBEntityRepositoryBaseSettings
    {
        [Required] public string ConnectionString { get; init; } = null!;
    }

    public sealed class MongoDBEntityRepositorySettings : MongoDBEntityRepositoryBaseSettings
    {
        public MongoDBEntityRepositorySettings() { }
        [Required] public string DatabaseName { get; init; } = null!;
        [Required] public string CollectionName { get; init; } = null!;
    }
}
