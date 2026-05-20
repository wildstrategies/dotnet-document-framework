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
        public void InvalidEntity()
        {
            TestEntity document = EntityFactory.InvalidTestEntity;
            try
            {
                Validator.ValidateObject(document, new ValidationContext(document));
                Assert.Fail("ValidationException should have been thrown.");
            }
            catch (ValidationException)
            {
                // Expected
            }
        }

        [TestMethod]
        public void InvalidValidSubEntityProperties()
        {
            TestEntity document = EntityFactory.InvalidTestEntity;
            document.SubEntity = new();
            try
            {
                Validator.ValidateObject(document, new ValidationContext(document));
                Assert.Fail("ValidationException should have been thrown.");
            }
            catch (ValidationException)
            {
                // Expected
            }
        }

        [TestMethod]
        public void InvalidSubEntityCollectionProperties()
        {
            TestEntity document = EntityFactory.InvalidTestEntity;
            document.SubEntity = EntityFactory.ValidTestEntity.SubEntity;
            document.Subentities = new List<TestSubentity>()
            {
                new TestSubentity()
            };

            try
            {
                Validator.ValidateObject(document, new ValidationContext(document), true);
                Assert.Fail("ValidationException should have been thrown.");
            }
            catch (ValidationException)
            {
                // Expected
            }
        }
    }
}
