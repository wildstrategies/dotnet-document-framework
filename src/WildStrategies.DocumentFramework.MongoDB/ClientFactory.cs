using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using WildStrategies.DocumentFramework.Serializer;

namespace WildStrategies.DocumentFramework
{
    public sealed class MongoDBDocumentFrameworkClient : MongoClient
    {
        private static readonly object _lockObject = new object();
        private static bool SerializationInitialized = false;

        private static readonly Dictionary<Type, IBsonSerializer> Serializers = new Dictionary<Type, IBsonSerializer>()
        {
            { typeof(DateOnly), new DateOnlySerializer() },
            { typeof(TimeOnly), new TimeOnlySerializer() }
        };

        private static void InitSerialization()
        {
            if (!SerializationInitialized)
            {
                BsonSerializer.RegisterSerializationProvider(new DocumentFrameworkBsonSerializationProvider());
                SerializationInitialized = true;
            }

            //
            //{
            //    lock (_lockObject)
            //    {
            //        foreach (var serializer in Serializers)
            //        {
            //            BsonSerializer.RegisterSerializer(serializer.Key, serializer.Value);
            //        }


            //        SerializationInitialized = true;
            //    }
            //}
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
