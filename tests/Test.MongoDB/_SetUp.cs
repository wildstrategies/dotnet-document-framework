using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Test.MongoDB
{
    [TestClass]
    public class SetUp
    {
        private static IConfiguration _configuration = null!;
        private static TestContext _testContext = null!;
        private static string _connectionString = null!;
        private static string _databaseName = null!;

        public static string ConnectionString => _connectionString;
        public static string DataBaseName => _databaseName;

        public static readonly Guid DocumentId = Guid.Parse("892d4eaa-4d03-4aed-84d1-7cf8baaecd0b");

        [AssemblyInitialize]
        public static async Task Initialize(TestContext testContext)
        {
            _configuration = new ConfigurationBuilder()
                .AddUserSecrets(typeof(SetUp).GetTypeInfo().Assembly)
                .Build();

            _testContext = testContext;
            _connectionString = _configuration.GetConnectionString("MongoDb");
            _databaseName = _configuration.GetValue<string>("Settings:DatabaseName");

            await CheckCollection();
        }

        private static async Task CheckCollection()
        {
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(_connectionString);
            /* TODO: Manage Allow insecure TLS using settings */
            settings.AllowInsecureTls = true;

            IMongoClient _client = new MongoClient(settings);
            IMongoDatabase _database = _client.GetDatabase(_databaseName);
            IMongoCollection<dynamic> _collection = _database.GetCollection<dynamic>(
                _configuration.GetValue<string>("Settings:RestaurantsCollectionName")
            );

            IMongoCollection<dynamic> _sourceCollection = _database.GetCollection<dynamic>(
                $"original_{_configuration.GetValue<string>("Settings:RestaurantsCollectionName")}"
            );

            if (_collection.AsQueryable().Count() == 0)
            {
                List<dynamic> toInsert = new List<dynamic>();
                foreach(var item in _sourceCollection.AsQueryable())
                {
                    item._id = Guid.NewGuid();
                    toInsert.Add(item);
                }

                await _collection.InsertManyAsync(toInsert);
            }
        }

        [AssemblyCleanup]
        public static void CleanUp()
        {

        }

        public static RestaurantsRepository GetRestaurantsRepository()
        {
            return new RestaurantsRepository(
                _connectionString,
                _databaseName,
                _configuration.GetValue<string>("Settings:RestaurantsCollectionName")
            );
        }

    }
}
