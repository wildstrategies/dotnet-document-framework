using Test.Shared.Entities;
using WildStrategies.DocumentFramework;

namespace Test.MongoDB.Repositories
{
    public class RestaurantsRepository : MongoDBEntityRepository<RestaurantEntity>
    {
        public RestaurantsRepository(MongoDBEntityRepositorySettings settings) : base(settings)
        {
        }
    }
}
