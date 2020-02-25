using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.DocumentFramework.Models;
using WildStrategies.DocumentFramework;

namespace Test.DocumentFramework
{
    [TestClass]
    [TestCategory("JsonSerialization")]
    public class JsonSerializationUnitTests
    {
        private static TestEntity TestEntity
        {
            get
            {
                TestEntity output = new TestEntity()
                {
                    Title = "TestEntityTitle",
                    Instant = NodaTime.SystemClock.Instance.GetCurrentInstant(),
                    Value = 13,
                    Child = new TestEntity()
                    {
                        Title = "TestEntityTitle",
                        Value = 12,
                    }
                };

                output.ValueObjects.Add(new TestValueObject() { ValueTitle = "P1" });
                output.ValueObjects.Add(new TestValueObject() { ValueTitle = "P2" });
                output.ValueObjects.Add(new TestValueObject() { ValueTitle = "P3" });

                output.Child.ValueObjects.Add(new TestValueObject() { ValueTitle = "C1" });
                output.Child.ValueObjects.Add(new TestValueObject() { ValueTitle = "C2" });
                output.Child.ValueObjects.Add(new TestValueObject() { ValueTitle = "C3" });
                output.Child.ValueObjects.Add(new TestValueObject() { ValueTitle = "C4" });


                return output;
            }
        }

        [TestMethod]
        public void ModelSerialization()
        {
            Document<TestEntity> document = TestEntity.CreateDocument();
            string serialized = document.ToJson();

            Assert.IsNotNull(serialized);
        }

        [TestMethod]
        public void ModelDeserialization()
        {
            Document<TestEntity> document = TestEntity.Child.CreateDocument();
            string serialized = document.ToJson();
            Document<TestEntity> deserialized = serialized.FromJson<TestEntity>();

            Assert.AreEqual(document, deserialized);
        }

        [TestMethod]
        public void ModelDeserializationWithChild()
        {
            Document<TestEntity> document = TestEntity.CreateDocument();
            string serialized = document.ToJson();
            Document<TestEntity> deserialized = serialized.FromJson<TestEntity>();

            Assert.AreEqual(document, deserialized);
        }
    }
}
