using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Test.Shared;

namespace Test.MongoDB
{
    [TestClass]
    public class ReadOnlyTests
    {
        private static RestaurantsRepository _repository = null!;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            _repository = new RestaurantsRepository(SetUp.RestaurantRepositorySettings);
        }

        [TestMethod]
        public void GetFirst()
        {
            var result = SetUp.GetRestaurantsRepository().AsQueryable().FirstOrDefault();
            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void GetCount()
        {
            var result = SetUp.GetRestaurantsRepository().AsQueryable().Count();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void GetTop100()
        {
            var result = SetUp.GetRestaurantsRepository().AsQueryable().Take(100).ToList();
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void CountWithAnyGrade()
        {
            var result = SetUp.GetRestaurantsRepository().AsQueryable().Where(x => x.grades.Any()).Count();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void CountWithGradeA()
        {
            var result = SetUp.GetRestaurantsRepository().AsQueryable().Where(x => x.grades.Any(g => g.grade == "A")).Count();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void CountWithScore2()
        {
            var result = SetUp.GetRestaurantsRepository().AsQueryable().Where(x => x.grades.Any(g => g.score == 2)).Count();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void GetWithScore2()
        {
            var result = SetUp.GetRestaurantsRepository().AsQueryable().Where(x => x.grades.Any(g => g.score == 2)).ToList();
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public async Task GetAllAsync()
        {
            var result = await SetUp.GetRestaurantsRepository().GetAsync();
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetAll()
        {
            var result = SetUp.GetRestaurantsRepository().AsQueryable().ToList();
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public async Task GetByIdAsync()
        {
            var result = SetUp.GetRestaurantsRepository().AsQueryable().FirstOrDefault();
            result = await SetUp.GetRestaurantsRepository().GetAsync(result?.Id ?? throw new System.Exception());

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetById()
        {
            var result = SetUp.GetRestaurantsRepository().AsQueryable().First();
            result = SetUp.GetRestaurantsRepository().AsQueryable().First(x => x.Id.Equals(result.Id));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task AddEmptyEntityAsync()
        {
            var entity = new RestaurantEntity();
            await Assert.ThrowsExceptionAsync<ValidationException>(() => SetUp.GetRestaurantsRepository().CreateOrUpdate(entity));
        }
    }
}