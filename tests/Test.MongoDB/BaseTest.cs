﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.MongoDB
{
    public abstract class BaseTest
    {
        private static RestaurantsRepository _repository = null!;
        protected static RestaurantsRepository Repository => _repository;

        protected static void Init(TestContext context)
        {
            _repository = new RestaurantsRepository(SetUp.RestaurantRepositorySettings);
        }
    }
}