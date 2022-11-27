using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.MongoDB.Repositories;

namespace Test.MongoDB
{
    public abstract class BaseTest
    {
        private static RestaurantsRepository _repository = null!;
        protected static RestaurantsRepository Repository => _repository;

#pragma warning disable IDE0060 // Remove unused parameter
        protected static void Init(TestContext context)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            _repository = new RestaurantsRepository(SetUp.RestaurantRepositorySettings);
        }
    }
}
