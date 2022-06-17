using MongoDB.Driver;
using WildStrategies.DocumentFramework.Serializer;

namespace WildStrategies.DocumentFramework
{
    internal static class ClientFactory
    {
        private static bool SerializationInitialized = false;

        private static void InitSerialization()
        {
            MongoDB.Bson.Serialization.BsonSerializer.RegisterSerializer(new DateOnlySerializer());
            MongoDB.Bson.Serialization.BsonSerializer.RegisterSerializer(new TimeOnlySerializer());
            SerializationInitialized = true;
        }

        public static MongoClient GetClient(string connectionString, bool allowInsecureTls)
        {
            if (!SerializationInitialized) InitSerialization();
            MongoClientSettings clientSettings = MongoClientSettings.FromConnectionString(connectionString);
            clientSettings.AllowInsecureTls = allowInsecureTls;

            return new MongoClient(clientSettings);
        }
    }
}
