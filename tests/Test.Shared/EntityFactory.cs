using Bogus;

namespace Test.Shared
{
    public static class EntityFactory
    {
        private static readonly string[] grades = new[] { "a", "b", "c", "d" };

        private static Faker<RestaurantEntity> restaurantFaker = null!;
        private static Faker<RestaurantAddress> restaurantAddressFaker = null!;
        private static Faker<RestaurantGrade> restaurantGradeFaker = null!;


        private static Faker<RestaurantEntity> GetRestaurantEntityGenerator()
        {
            restaurantFaker ??= new Faker<RestaurantEntity>()
                    .RuleForType(typeof(string), f => f.Random.Word())
                    .RuleFor(f => f.address, f => GetRestaurantAddressGenerator())
                    .RuleFor(f => f.grades, f => GetRestaurantGradeGenerator().GenerateBetween(0, 10));

            return restaurantFaker;
        }

        private static Faker<RestaurantGrade> GetRestaurantGradeGenerator()
        {
            restaurantGradeFaker ??= new Faker<RestaurantGrade>()
                    .RuleFor(f => f.date, f => f.Date.Past())
                    .RuleFor(f => f.score, f => f.Random.Int(1, 5))
                    .RuleFor(f => f.grade, f => f.PickRandom(grades));

            return restaurantGradeFaker;
        }

        private static Faker<RestaurantAddress> GetRestaurantAddressGenerator()
        {
            restaurantAddressFaker ??= new Faker<RestaurantAddress>()
                    .RuleForType(typeof(string), f => f.Random.Word())
                    .RuleFor(x => x.coord, f => new double[] {
                        f.Random.Double(),
                        f.Random.Double()
                    });

            return restaurantAddressFaker;
        }

        public static RestaurantEntity CreateRestaurant()
        {
            return GetRestaurantEntityGenerator().Generate();
        }

        public static IEnumerable<RestaurantEntity> CreateRestaurants(int count)
        {
            return GetRestaurantEntityGenerator().Generate(count);
        }
    }
}
