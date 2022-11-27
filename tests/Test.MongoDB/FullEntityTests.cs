using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using Test.MongoDB.Repositories;
using Test.Shared;
using Test.Shared.Models;

namespace Test.MongoDB
{
    [TestClass]
    public class FullEntityTests
    {
        protected static TestDictionaryEntitiesRepository Repository { get; private set; } = null!;

        [ClassInitialize]
#pragma warning disable IDE0060 // Remove unused parameter
        public static void Init(TestContext context)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            Repository = new TestDictionaryEntitiesRepository(SetUp.TestEntityRepositorySettings);
        }

        [TestMethod]
        public async Task SerializeEntity()
        {
            await Repository.InsertAsync(EntityFactory.ValidTestEntity);
        }

        [TestMethod]
        public async Task DeserializeEntity()
        {
            var entity = EntityFactory.ValidTestEntity;
            await Repository.InsertAsync(entity);
            var deserializedEntity = await Repository.GetAsync(entity.Id);
            Assert.IsTrue(entity != null);
            Assert.AreEqual(entity.Id, deserializedEntity?.Id);
            Assert.AreEqual(entity.GuidIntDictionary.First().Key, entity.GuidIntDictionary.First().Key);
            Assert.AreEqual(entity.GuidObjectDictionary.First().Key, entity.GuidObjectDictionary.First().Key);
        }
    }
}