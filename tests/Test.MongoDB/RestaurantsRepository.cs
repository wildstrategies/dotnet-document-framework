using Test.Shared;
using WildStrategies.DocumentFramework;

namespace Test.MongoDB
{
    public class RestaurantsRepository : MongoDBEntityRepository<RestaurantEntity>
    {
        public RestaurantsRepository(MongoDBEntityRepositorySettings settings) : base(settings)
        {
        }

        public RestaurantsRepository(string connectionString, string databaseName, string collectionName)
            : base(connectionString, databaseName, collectionName)
        {
        }
    }
}
