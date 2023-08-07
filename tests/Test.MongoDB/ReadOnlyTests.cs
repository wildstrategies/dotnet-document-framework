using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using Test.Shared.Entities;
using static System.Formats.Asn1.AsnWriter;

namespace Test.MongoDB
{
    [TestClass]
    public class ReadOnlyTests : BaseTest
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            Init(context);
        }

        [TestMethod]
        public void GetFirst()
        {
            RestaurantEntity? result = Repository.AsQueryable().FirstOrDefault();
            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void GetCount()
        {
            int result = Repository.AsQueryable().Count();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void GetTop100()
        {
            System.Collections.Generic.List<RestaurantEntity>? result = Repository.AsQueryable().Take(100).ToList();
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void CountWithAnyGrade()
        {
            int result = Repository.AsQueryable().Where(x => x.grades.Any()).Count();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void CountWithGrade()
        {
            var grade = Repository.AsQueryable().Max(x => x.grades.Max(y => y.grade));
            int result = Repository.AsQueryable().Where(x => x.grades.Any(g => g.grade == grade)).Count();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void GetWithScore()
        {
            var score = Repository.AsQueryable().Max(x => x.grades.Max(y => y.score));
            System.Collections.Generic.List<RestaurantEntity>? result = Repository.AsQueryable().Where(x => x.grades.Any(g => g.score == score)).ToList();
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public async Task GetAllAsync()
        {
            System.Collections.Generic.IEnumerable<RestaurantEntity>? result = await Repository.GetAsync();
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetAll()
        {
            System.Collections.Generic.List<RestaurantEntity>? result = Repository.AsQueryable().ToList();
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public async Task GetByIdAsync()
        {
            RestaurantEntity? result = Repository.AsQueryable().FirstOrDefault();
            result = await Repository.GetAsync(result?.Id ?? throw new System.Exception());

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetById()
        {
            RestaurantEntity? result = Repository.AsQueryable().First();
            result = Repository.AsQueryable().First(x => x.Id.Equals(result.Id));

            Assert.IsNotNull(result);
        }
    }
}