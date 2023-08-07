using Test.Shared.Models;
using WildStrategies.DocumentFramework;

namespace Test.MongoDB.Repositories
{
    public class TestEntitiesRepository : MongoDBEntityRepository<TestEntity>
    {
        public TestEntitiesRepository(MongoDBEntityRepositorySettings settings) : base(settings)
        {
        }
    }
}
