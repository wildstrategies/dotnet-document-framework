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
        [ClassInitialize] public static void Initialize(TestContext context) => Init(context);

        [TestMethod]
        public async Task AddEmptyEntityAsync()
        {
            var entity = new RestaurantEntity();
            await Assert.ThrowsExceptionAsync<ValidationException>(() => Repository.UpdateAsync(entity));
        }

        [TestMethod]
        public async Task UpdateEntityAsync()
        {
            var entity = Repository.AsQueryable().First();
            var count = Repository.AsQueryable().Count();
            var lastUpdateTime = entity.LastUpdateTime;
            await Task.Delay(2);
            await Repository.UpdateAsync(entity);
            Assert.AreNotEqual(entity.LastUpdateTime, lastUpdateTime);
            Assert.AreEqual(Repository.AsQueryable().Count(), count);
        }

        [TestMethod]
        public async Task CreateEntityAsync()
        {
            var count = Repository.AsQueryable().Count();
            var entity = EntityFactory.CreateRestaurant();
            await Repository.InsertAsync(entity);
            Assert.AreNotEqual(Repository.AsQueryable().Count(), count);
        }

        [TestMethod]
        public async Task DuplicateEntityMustFailsAsync()
        {
            var entity = Repository.AsQueryable().First();
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
            var entity = EntityFactory.CreateRestaurant();
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
            var entities = EntityFactory.CreateRestaurants(count);
            await Repository.InsertManyAsync(entities);
        }

        [TestMethod]
        [DataRow(1)]
        public async Task InsertManyAsyncMustFails(int count)
        {
            var entities = EntityFactory.CreateRestaurants(count).ToList();
            entities.Add(Repository.AsQueryable().First());
            try
            {
                await Repository.InsertManyAsync(entities); Assert.Fail();
            }
            catch { }
        }
    }
}