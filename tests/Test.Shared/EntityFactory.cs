using Bogus;
using Test.Shared.Entities;
using Test.Shared.Models;

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

        public static TestDictionaryEntity ValidTestDictionaryEntity
        {
            get
            {
                return new TestDictionaryEntity()
                {
                    MetadataObject = new TestEntityMetadata()
                    {
                        { "Test1", "Value1" },
                        { "Test2", "Value2" },
                        { "Test3", "Value3" }
                    },
                    MetadataSimple = new Dictionary<string, string>()
                    {
                        { "Test1", "Value1" },
                        { "Test2", "Value2" },
                        { "Test3", "Value3" }
                    },
                    StringObjectDictionary = new TestEntityStringObjectDictionary<TestObject>()
                    {
                        { "Object1", new TestObject() },
                        { "Object2", new TestObject() },
                        { "Object3", new TestObject() },
                    },
                    GuidIntDictionary = new()
                    {
                        { Guid.NewGuid(), 1 },
                        { Guid.NewGuid(), 2 },
                        { Guid.NewGuid(), 3 },
                    },
                    GuidObjectDictionary = new()
                    {
                        { Guid.NewGuid(), new TestObject(){ Name="Name1" } },
                        { Guid.NewGuid(), new TestObject(){ Name="Name2" } },
                        { Guid.NewGuid(), new TestObject(){ Name="Name3" } },
                        { Guid.NewGuid(), new TestObject(){ Name="Name4" } },
                        { Guid.NewGuid(), new TestObject(){ Name="Name5" } }
                    }
                };
            }
        }

        public static TestEntity ValidTestEntity
        {
            get
            {
                var document = InvalidTestEntity;
                document.SubEntity = new TestSubentity()
                {
                    RequiredString = "AAAA",
                    MaxStringLength = 10
                };
                document.Subentities = new[] {
                    new TestSubentity() {
                        RequiredString = "AAAA",
                        MaxStringLength = 2
                    }
                };
                document.Child = null;
                document.NotValidatableObject = new TestObject();
                return document;
            }
        }

        public static TestEntity InvalidTestEntity
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
                    },
                    MetadataObject = new TestEntityMetadata()
                    {
                        { "Test1", "Value1" },
                        { "Test2", "Value2" },
                        { "Test3", "Value3" }
                    },
                    MetadataSimple = new Dictionary<string, string>()
                    {
                        { "Test1", "Value1" },
                        { "Test2", "Value2" },
                        { "Test3", "Value3" }
                    },
                    StringObjectDictionary = new TestEntityStringObjectDictionary<TestObject>()
                    {
                        { "Object1", new TestObject() },
                        { "Object2", new TestObject() },
                        { "Object3", new TestObject() },
                    },
                    GuidIntDictionary = new()
                    {
                        { Guid.NewGuid(), 1 },
                        { Guid.NewGuid(), 2 },
                        { Guid.NewGuid(), 3 },
                    },
                    GuidObjectDictionary = new()
                    {
                        { Guid.NewGuid(), new TestObject(){ Name="Name1" } },
                        { Guid.NewGuid(), new TestObject(){ Name="Name2" } },
                        { Guid.NewGuid(), new TestObject(){ Name="Name3" } },
                        { Guid.NewGuid(), new TestObject(){ Name="Name4" } },
                        { Guid.NewGuid(), new TestObject(){ Name="Name5" } }
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
    }
}
