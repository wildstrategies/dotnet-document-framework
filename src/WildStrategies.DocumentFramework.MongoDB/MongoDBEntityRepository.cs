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

        protected MongoDBEntityRepository(string connectionString, string databaseName, string collectionName, bool allowInsecureTls) :
            base(connectionString, databaseName, collectionName, allowInsecureTls)
        {
        }

        private static void ValidateEntity(T entity)
        {
            Validator.ValidateObject(entity, new ValidationContext(entity));

            entity.GetType().GetProperty(nameof(entity.LastUpdateTime))?.SetValue(entity, DateTime.UtcNow);
        }

        public Task DeleteAsync(T entity)
        {
            return _collection.DeleteOneAsync(GetFilterById(entity.Id));
        }

        public Task UpdateAsync(T entity)
        {
            MongoDBEntityRepository<T>.ValidateEntity(entity);
            return _collection.ReplaceOneAsync(GetFilterById(entity.Id), entity, new ReplaceOptions()
            {
                IsUpsert = false
            }).ContinueWith(t =>
            {
                if (t.Result.ModifiedCount == 0)
                {
                    throw new Exception("Entity not found");
                }
            });
        }

        public Task InsertAsync(T entity)
        {
            MongoDBEntityRepository<T>.ValidateEntity(entity);
            return _collection.InsertOneAsync(entity);
        }

        public Task InsertManyAsync(IEnumerable<T> entities)
        {
            foreach (T? entity in entities)
            {
                MongoDBEntityRepository<T>.ValidateEntity(entity);
            }

            return _collection.InsertManyAsync(entities);
        }
    }
}
