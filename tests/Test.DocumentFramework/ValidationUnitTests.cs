using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using Test.DocumentFramework.Models;

namespace Test.DocumentFramework
{
    [TestClass]
    [TestCategory("EntityValidation")]
    public class ValidationUnitTests
    {
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
