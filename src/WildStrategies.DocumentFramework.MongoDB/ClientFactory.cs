using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using WildStrategies.DocumentFramework.Serializer;

namespace WildStrategies.DocumentFramework
{
    public sealed class MongoDBDocumentFrameworkClient : MongoClient
    {
        private static bool SerializationInitialized = false;

        private static void InitSerialization()
        {
            if (!SerializationInitialized)
            {
                BsonSerializer.RegisterSerializationProvider(new DocumentFrameworkBsonSerializationProvider());
                SerializationInitialized = true;
            }
        }

        private static MongoClientSettings GetMongoClientSettings(MongoDBEntityRepositoryBaseSettings settings)
        {
            MongoClientSettings output = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            output.AllowInsecureTls = settings.AllowInsecureTls;
            return output;
        }

        public MongoDBDocumentFrameworkClient(MongoDBEntityRepositoryBaseSettings settings)
            : base(GetMongoClientSettings(settings))
        {
            InitSerialization();
        }
    }
}
