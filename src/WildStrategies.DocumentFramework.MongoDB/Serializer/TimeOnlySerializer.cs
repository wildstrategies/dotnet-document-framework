using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace WildStrategies.DocumentFramework.Serializer
{
    internal class TimeOnlySerializer : StructSerializerBase<TimeOnly>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TimeOnly value) =>
            context.Writer.WriteString(value.SerializeAsString());

        public override TimeOnly Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) =>
            TimeOnly.Parse(context.Reader.ReadString());
    }
}
