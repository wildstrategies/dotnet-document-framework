using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.DocumentFramework.Models;
using WildStrategies.DocumentFramework;

namespace Test.DocumentFramework
{
    [TestClass]
    [TestCategory("JsonSerialization")]
    public class JsonSerializationUnitTests
    {
        private readonly static TestEntity testEntity = new TestEntity()
        {
            Title = "TestEntityTitle",
            Instant = NodaTime.SystemClock.Instance.GetCurrentInstant(),
            Value = 13,
            Child = new TestEntity()
            {
                Title = "TestEntityTitle",
                Value = 12
            }
        };

        [TestMethod]
        public void ModelSerialization()
        {
            var document = testEntity.CreateDocument();
            string serialized = document.ToJson();

            Assert.IsNotNull(serialized);
        }

        [TestMethod]
        public void ModelDeserialization()
        {
            var document = testEntity.CreateDocument();
            string serialized = document.ToJson();
            Document<TestEntity> deserialized = serialized.FromJson<TestEntity>();

            Assert.AreEqual(document, deserialized);
        }
    }
}
