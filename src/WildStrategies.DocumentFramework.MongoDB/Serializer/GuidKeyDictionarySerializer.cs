using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;

namespace WildStrategies.DocumentFramework.Serializer
{
    internal class GuidKeyDictionarySerializer<T> : DictionarySerializerBase<Dictionary<Guid, T>>
    {
        public GuidKeyDictionarySerializer() :
            base(DictionaryRepresentation.Document, new GuidStringSerializer(), new ObjectSerializer(t => t == typeof(T)))
        {

        }

        protected override Dictionary<Guid, T> CreateInstance() => new();
    }
}
