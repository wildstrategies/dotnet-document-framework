using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using Test.Shared;

namespace Test.MongoDB
{
    [TestClass]
    public class DateAndTimeOnlyTests : BaseTest
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            Init(context);
        }

        [TestMethod]
        public async Task CheckSerialization()
        {

            RestaurantEntity entity = EntityFactory.CreateRestaurant();
            var date = DateOnly.FromDateTime(DateTime.Now);
            var time = TimeOnly.FromDateTime(DateTime.Now);
            entity.SetDateAndTimeOnly(date, time);

            await Repository.InsertAsync(entity);
            var saved = await Repository.GetAsync(entity.Id);

            Assert.AreEqual(entity.dateOnly, saved?.dateOnly);
            Assert.AreEqual(entity.timeOnly, saved?.timeOnly);
        }
    }
}