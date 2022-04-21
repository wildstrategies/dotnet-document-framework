using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Test.Shared;

namespace Test.MongoDB
{
    [TestClass]
    public class WriteTests : BaseTest
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            Init(context);
        }

        [TestMethod]
        public async Task AddEmptyEntityAsync()
        {
            RestaurantEntity? entity = new();
            await Assert.ThrowsExceptionAsync<ValidationException>(() => Repository.UpdateAsync(entity));
        }

        [TestMethod]
        public async Task UpdateEntityAsync()
        {
            RestaurantEntity? entity = Repository.AsQueryable().First();
            int count = Repository.AsQueryable().Count();
            DateTime lastUpdateTime = entity.LastUpdateTime;
            await Task.Delay(2);
            await Repository.UpdateAsync(entity);
            Assert.AreNotEqual(entity.LastUpdateTime, lastUpdateTime);
            Assert.AreEqual(Repository.AsQueryable().Count(), count);
        }

        [TestMethod]
        public async Task CreateEntityAsync()
        {
            int count = Repository.AsQueryable().Count();
            RestaurantEntity? entity = EntityFactory.CreateRestaurant();
            await Repository.InsertAsync(entity);
            Assert.AreNotEqual(Repository.AsQueryable().Count(), count);
        }

        [TestMethod]
        public async Task DuplicateEntityMustFailsAsync()
        {
            RestaurantEntity? entity = Repository.AsQueryable().First();
            try
            {
                await Repository.InsertAsync(entity);
                Assert.Fail();
            }
            catch { }
        }

        [TestMethod]
        public async Task NotExistingEntityUpdateMustFailsAsync()
        {
            RestaurantEntity? entity = EntityFactory.CreateRestaurant();
            try
            {
                await Repository.UpdateAsync(entity);
                Assert.Fail();
            }
            catch { }
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(10)]
        [DataRow(100)]
        [DataRow(1000)]
        public async Task InsertManyAsync(int count)
        {
            System.Collections.Generic.IEnumerable<RestaurantEntity>? entities = EntityFactory.CreateRestaurants(count);
            await Repository.InsertManyAsync(entities);
        }

        [TestMethod]
        [DataRow(1)]
        public async Task InsertManyAsyncMustFails(int count)
        {
            System.Collections.Generic.List<RestaurantEntity>? entities = EntityFactory.CreateRestaurants(count).ToList();
            entities.Add(Repository.AsQueryable().First());
            try
            {
                await Repository.InsertManyAsync(entities);
                Assert.Fail();
            }
            catch { }
        }
    }
}