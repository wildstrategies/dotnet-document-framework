using Test.Shared;
using WildStrategies.DocumentFramework;

namespace Test.MongoDB
{
    public class RestaurantsRepository : MongoDBEntityRepository<RestaurantEntity>
    {
        public RestaurantsRepository(MongoDBEntityRepositorySettings settings) : base(settings)
        {
        }
    }
}
