using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WildStrategies.DocumentFramework
{

    public abstract class MongoDBEntitytReadonlyRepository<T> : IEntityReadOnlyRepository<T> where T : Entity
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        protected readonly IMongoCollection<T> _collection;
        protected FilterDefinition<T> GetFilterById(Guid id)
        {
            return Builders<T>.Filter.Eq(nameof(Entity.Id), id);
        }

        public MongoDBEntitytReadonlyRepository(
            string connectionString,
            string databaseName,
            string collectionName
        )
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"'{nameof(connectionString)}' cannot be null or empty.", nameof(connectionString));
            }

            if (string.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentException($"'{nameof(databaseName)}' cannot be null or empty.", nameof(databaseName));
            }

            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentException($"'{nameof(collectionName)}' cannot be null or empty.", nameof(collectionName));
            }

            MongoClientSettings settings = MongoClientSettings.FromConnectionString(connectionString);
            /* TODO: Manage Allow insecure TLS using settings */
            settings.AllowInsecureTls = true;

            _client = new MongoClient(settings);

            _database = _client.GetDatabase(databaseName);
            _collection = _database.GetCollection<T>(collectionName);
        }

        public MongoDBEntitytReadonlyRepository(MongoDBEntityRepositorySettings settings) :
            this(settings.ConnectionString, settings.DatabaseName, settings.CollectionName)
        { }

        public MongoDBEntitytReadonlyRepository(IOptions<MongoDBEntityRepositorySettings> settings) :
                this(settings.Value.ConnectionString, settings.Value.DatabaseName, settings.Value.CollectionName)
        { }

        public IQueryable<T> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        public Task<IEnumerable<T>> GetAsync()
        {
            return _collection.Find(new BsonDocument()).ToListAsync().ContinueWith(task => task.Result.AsEnumerable());
        }

        public Task<T?> GetAsync(Guid id)
        {
            return Task.FromResult(AsQueryable().FirstOrDefault(x => x.Id.Equals(id)));
        }

        public ValueTask DisposeAsync()
        {
            GC.SuppressFinalize(this);
            return ValueTask.CompletedTask;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
