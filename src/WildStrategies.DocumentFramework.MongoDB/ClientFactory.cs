using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using WildStrategies.DocumentFramework.Serializer;

namespace WildStrategies.DocumentFramework
{
    public static class MongoDBDocumentFrameworkClient
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

        public static IMongoClient Create(MongoDBEntityRepositoryBaseSettings settings)
        {
            InitSerialization();
            return new MongoClient(GetMongoClientSettings(settings));
        }
    }
}
