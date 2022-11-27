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
            TestEntity document = EntityFactory.InvalidTestEntity;
            Assert.ThrowsException<ValidationException>(() =>
                Validator.ValidateObject(document, new ValidationContext(document))
            );
        }

        [TestMethod]
        public void ValidSubEntityProperties()
        {
            TestEntity document = EntityFactory.InvalidTestEntity;
            document.SubEntity = new();
            Assert.ThrowsException<ValidationException>(() =>
                Validator.ValidateObject(document, new ValidationContext(document))
            );
        }

        [TestMethod]
        public void ValidSubEntityCollectionProperties()
        {
            TestEntity document = EntityFactory.InvalidTestEntity;
            document.SubEntity = EntityFactory.ValidTestEntity.SubEntity;
            document.Subentities = new List<TestSubentity>()
            {
                new TestSubentity()
            };

            Assert.ThrowsException<ValidationException>(() =>
                Validator.ValidateObject(document, new ValidationContext(document), true)
            );
        }
    }
}
