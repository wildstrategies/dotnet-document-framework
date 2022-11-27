using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Shared;
using Test.Shared.Models;
using WildStrategies.DocumentFramework;

namespace Test.DocumentFramework
{
    [TestClass]
    [TestCategory("JsonSerialization")]
    public class JsonSerializationUnitTests
    {
        private static bool CompareEntities(TestEntity source, TestEntity deserialized)
        {
            if (!source.Id.Equals(deserialized.Id))
            {
                return false;
            }

            if (source.Enumerable.Union(deserialized.Enumerable).Count() != source.Enumerable.Count())
            {
                return false;
            }

            if (source.Dictionary.Union(deserialized.Dictionary).Count() != source.Dictionary.Count)
            {
                return false;
            }

            if (source.Child?.Id != deserialized.Child?.Id)
            {
                return false;
            }

            if (source.ValueObjects != null)
            {
                if (source.ValueObjects.Select(x => x.Id).Union(deserialized.ValueObjects.Select(x => x.Id)).Count() != source.ValueObjects.Count)
                {
                    return false;
                }
            }

            return true;
        }

        [TestMethod]
        public void ModelSerialization()
        {
            TestEntity document = EntityFactory.ValidTestEntity;
            string serialized = document.ToJson();

            Assert.IsNotNull(serialized);
        }

        [TestMethod]
        public void ModelDeserialization()
        {
            TestEntity document = EntityFactory.ValidTestEntity;
            document.ResetChilds();
            string serialized = document.ToJson();
            TestEntity deserialized = serialized.FromJson<TestEntity>();

            Assert.IsTrue(CompareEntities(document, deserialized));
        }

        [TestMethod]
        public void ModelDeserializationWithChild()
        {
            TestEntity document = EntityFactory.ValidTestEntity;
            string serialized = document.ToJson();
            TestEntity deserialized = serialized.FromJson<TestEntity>();

            Assert.IsTrue(CompareEntities(document, deserialized));
        }
    }
}
