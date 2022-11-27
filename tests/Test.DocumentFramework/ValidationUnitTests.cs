using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using Test.Shared;
using Test.Shared.Models;

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
            TestEntity document = EntityFactory.TestEntity;
            Assert.ThrowsException<ValidationException>(() =>
                Validator.ValidateObject(document, new ValidationContext(document))
            );
        }

        [TestMethod]
        public void ValidSubEntityProperties()
        {
            TestEntity document = EntityFactory.TestEntity;
            document.SubEntity = new();
            Assert.ThrowsException<ValidationException>(() =>
                Validator.ValidateObject(document, new ValidationContext(document))
            );
        }

        [TestMethod]
        public void ValidSubEntityCollectionProperties()
        {
            TestEntity document = EntityFactory.TestEntity;
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
