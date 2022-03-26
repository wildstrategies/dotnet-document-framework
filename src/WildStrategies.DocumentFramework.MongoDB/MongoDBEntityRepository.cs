using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;

namespace WildStrategies.DocumentFramework
{
    public abstract class MongoDBEntityRepository<T> : MongoDBEntitytReadonlyRepository<T>, IEntityRepository<T> where T : Entity
    {
        protected MongoDBEntityRepository(MongoDBEntityRepositorySettings settings) : base(settings)
        {
        }

        protected MongoDBEntityRepository(IOptions<MongoDBEntityRepositorySettings> settings) : base(settings)
        {
        }

        protected MongoDBEntityRepository(string connectionString, string databaseName, string collectionName) : base(connectionString, databaseName, collectionName)
        {
        }

        public async Task<T> CreateOrUpdate(T entity)
        {
            Validator.ValidateObject(entity, new ValidationContext(entity));

            entity.GetType().GetProperty(nameof(entity.LatsUpdateTime))?.SetValue(entity, DateTime.UtcNow);

            var result = await _collection.ReplaceOneAsync(GetFilterById(entity.Id), entity, new ReplaceOptions()
            {
                IsUpsert = true
            });

            return entity;
        }

        public Task Delete(T entity) => _collection.DeleteOneAsync(GetFilterById(entity.Id));
    }
}
