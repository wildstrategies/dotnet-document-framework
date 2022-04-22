using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.DocumentFramework.Models;
using WildStrategies.DocumentFramework;

namespace Test.DocumentFramework
{
    [TestClass]
    [TestCategory("JsonSerialization")]
    public class JsonSerializationUnitTests
    {
        public static TestEntity TestEntity
        {
            get
            {
                TestEntity output = new()
                {
                    Title = "TestEntityTitle",
                    Value = 13,
                    Child = new TestEntity()
                    {
                        Title = "TestEntityTitle",
                        Value = 12,
                    },
                    Enumerable = new[] { "1", "2", "3" },
                    Dictionary = new Dictionary<string, string>()
                    {
                        { "sample", "sample_value" },
                        { "sample2", "sample_value2" }
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
            TestEntity document = new();
            string serialized = document.ToJson();

            Assert.IsNotNull(serialized);
        }

        [TestMethod]
        public void ModelDeserialization()
        {
            TestEntity document = TestEntity;
            document.ResetChilds();
            string serialized = document.ToJson();
            TestEntity deserialized = serialized.FromJson<TestEntity>();

            Assert.IsTrue(CompareEntities(document, deserialized));
        }

        [TestMethod]
        public void ModelDeserializationWithChild()
        {
            TestEntity document = TestEntity;
            string serialized = document.ToJson();
            TestEntity deserialized = serialized.FromJson<TestEntity>();

            Assert.IsTrue(CompareEntities(document, deserialized));
        }
    }
}
