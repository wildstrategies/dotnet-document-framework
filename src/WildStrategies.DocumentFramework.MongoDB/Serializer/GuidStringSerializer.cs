using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace WildStrategies.DocumentFramework.Serializer
{
    internal class GuidStringSerializer : IBsonSerializer<Guid>
    {
        private readonly StringSerializer serializer = new StringSerializer();

        public Type ValueType => typeof(Guid);

        public Guid Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
            => Guid.Parse(serializer.Deserialize(context, args));

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Guid value)
            => serializer.Serialize(context, args, value.ToString());

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
            => serializer.Serialize(context, args, value.ToString());

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
            => Guid.Parse(serializer.Deserialize(context, args));
    }
}
