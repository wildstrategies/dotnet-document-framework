using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.MongoDB
{
    public abstract class BaseTest
    {
        protected static RestaurantsRepository _repository = null!;
        protected static RestaurantsRepository Repository => _repository;

        protected static void Init(TestContext context)
        {
            _repository = new RestaurantsRepository(SetUp.RestaurantRepositorySettings);
        }
    }
}
