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

        private void ValidateEntity(T entity)
        {
            Validator.ValidateObject(entity, new ValidationContext(entity));

            entity.GetType().GetProperty(nameof(entity.LastUpdateTime))?.SetValue(entity, DateTime.UtcNow);
        }

        public Task DeleteAsync(T entity) => _collection.DeleteOneAsync(GetFilterById(entity.Id));

        public Task UpdateAsync(T entity)
        {
            ValidateEntity(entity);
            return _collection.ReplaceOneAsync(GetFilterById(entity.Id), entity, new ReplaceOptions()
            {
                IsUpsert = false
            }).ContinueWith(t =>
            {
                if (t.Result.ModifiedCount == 0) throw new Exception("Entity not found");
            });
        }

        public Task InsertAsync(T entity)
        {
            ValidateEntity(entity);
            return _collection.InsertOneAsync(entity);
        }

        public Task InsertManyAsync(IEnumerable<T> entities)
        {
            foreach(var entity in entities)
            {
                ValidateEntity(entity);
            }

            return _collection.InsertManyAsync(entities);
        }
    }
}
