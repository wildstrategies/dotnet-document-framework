using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WildStrategies.DocumentFramework
{

    public class MongoDBEntitytReadonlyRepository<T> : IEntityReadOnlyRepository<T> where T : Entity
    {
        protected readonly IMongoCollection<T> _collection;

        protected FilterDefinition<T> GetFilterById(Guid id)
        {
            return Builders<T>.Filter.Eq(nameof(Entity.Id), id);
        }

        public MongoDBEntitytReadonlyRepository(MongoDBEntityRepositorySettings settings)
        {
            if (string.IsNullOrEmpty(settings.ConnectionString))
            {
                throw new ArgumentException($"'{nameof(settings.ConnectionString)}' cannot be null or empty.", nameof(settings.ConnectionString));
            }

            if (string.IsNullOrEmpty(settings.DatabaseName))
            {
                throw new ArgumentException($"'{nameof(settings.DatabaseName)}' cannot be null or empty.", nameof(settings.DatabaseName));
            }
            if (string.IsNullOrEmpty(settings.CollectionName))
            {
                throw new ArgumentException($"'{nameof(settings.CollectionName)}' cannot be null or empty.", nameof(settings.CollectionName));
            }

            MongoDBDocumentFrameworkClient _client = new MongoDBDocumentFrameworkClient(settings);
            IMongoDatabase _database = _client.GetDatabase(settings.DatabaseName);
            _collection = _database.GetCollection<T>(settings.CollectionName);

        }

        public MongoDBEntitytReadonlyRepository(IOptions<MongoDBEntityRepositorySettings> settings) : this(settings.Value) { }

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
