using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using Test.DocumentFramework.Models;

namespace Test.DocumentFramework
{
    [TestClass]
    [TestCategory("EntityValidation")]
    public class ValidationUnitTests
    {
        private static TestEntity TestEntity
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

        [TestMethod]
        public void ValidEntity()
        {
            TestEntity document = new();
            Assert.ThrowsException<ValidationException>(() =>
                Validator.ValidateObject(document, new ValidationContext(document))
            );
        }

        [TestMethod]
        public void ValidSubEntity()
        {
            TestEntity document = JsonSerializationUnitTests.TestEntity;
            Assert.ThrowsException<ValidationException>(() =>
                Validator.ValidateObject(document, new ValidationContext(document))
            );
        }

        [TestMethod]
        public void ValidSubEntityProperties()
        {
            TestEntity document = JsonSerializationUnitTests.TestEntity;
            document.SubEntity = new();
            Assert.ThrowsException<ValidationException>(() =>
                Validator.ValidateObject(document, new ValidationContext(document))
            );
        }

        [TestMethod]
        public void ValidSubEntityCollectionProperties()
        {
            TestEntity document = JsonSerializationUnitTests.TestEntity;
            document.SubEntity = new TestSubentity()
            {
                RequiredString = "AAAA",
                MaxStringLength = 10
            };
            document.Subentities = new[] { new TestSubentity() };
            document.Child = null;
            document.NotValidatableObject = new TestObject();

            Assert.ThrowsException<ValidationException>(() =>
                Validator.ValidateObject(document, new ValidationContext(document), true)
            );
        }
    }
}
