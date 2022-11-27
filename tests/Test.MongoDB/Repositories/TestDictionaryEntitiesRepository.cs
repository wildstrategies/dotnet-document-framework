using Test.Shared.Models;
using WildStrategies.DocumentFramework;

namespace Test.MongoDB.Repositories
{
    public class TestDictionaryEntitiesRepository : MongoDBEntityRepository<TestDictionaryEntity>
    {
        public TestDictionaryEntitiesRepository(MongoDBEntityRepositorySettings settings) : base(settings)
        {
        }
    }
}
