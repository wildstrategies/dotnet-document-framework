using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WildStrategies.DocumentFramework;

namespace Test.MongoDB
{
    [TestClass]
    public class SetUp
    {
#pragma warning disable IDE0052 // Remove unread private members
        private static TestContext _testContext = null!;
#pragma warning restore IDE0052 // Remove unread private members

        private static IConfiguration _configuration = null!;
        private static string _connectionString = null!;
        private static string _databaseName = null!;
        private static string _collectionName = null!;

        public static MongoDBEntityRepositorySettings RestaurantRepositorySettings => new()
        {
            ConnectionString = _connectionString,
            DatabaseName = _databaseName,
            CollectionName = _collectionName
        };

        public static readonly Guid DocumentId = Guid.Parse("892d4eaa-4d03-4aed-84d1-7cf8baaecd0b");

        [AssemblyInitialize]
        public static async Task Initialize(TestContext testContext)
        {
            _configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables("DOTNET_")
                .AddUserSecrets(typeof(SetUp).GetTypeInfo().Assembly, true)
                .Build();

            _testContext = testContext;
            _connectionString = _configuration.GetConnectionString("MongoDb");
            _databaseName = _configuration.GetValue<string>("Settings:DatabaseName");
            _collectionName = _configuration.GetValue<string>("Settings:RestaurantsCollectionName");

            await CheckCollection();
        }

        private static async Task CheckCollection()
        {
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(RestaurantRepositorySettings.ConnectionString);
            /* TODO: Manage Allow insecure TLS using settings */
            settings.AllowInsecureTls = true;

            IMongoClient _client = new MongoClient(settings);
            IMongoDatabase _database = _client.GetDatabase(_databaseName);
            IMongoCollection<dynamic> _collection = _database.GetCollection<dynamic>(
                    RestaurantRepositorySettings.CollectionName
                );

            await _database.DropCollectionAsync(RestaurantRepositorySettings.CollectionName);

            if (!_collection.AsQueryable().Any())
            {
                string? jsonData = File.ReadAllText(
                    $"{Directory.GetCurrentDirectory()}/Data/restaurants.json"
                );

                IEnumerable<dynamic>? data = BsonSerializer.Deserialize<IEnumerable<dynamic>>(jsonData);
                await _collection.InsertManyAsync(new dynamic[] { data.First() });
            }
        }

        [AssemblyCleanup]
        public static void CleanUp()
        {

        }
    }
}
