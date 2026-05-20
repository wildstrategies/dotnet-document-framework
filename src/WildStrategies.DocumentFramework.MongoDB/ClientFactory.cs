using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using WildStrategies.DocumentFramework.Serializer;

namespace WildStrategies.DocumentFramework
{
    public static class MongoDBDocumentFrameworkClient
    {
        private static bool _serializationInitialized = false;

        private static void InitSerialization()
        {
            if (_serializationInitialized) return;
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));
            BsonSerializer.RegisterSerializationProvider(new DocumentFrameworkBsonSerializationProvider());
            _serializationInitialized = true;
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
