using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace WildStrategies.DocumentFramework.Serializer
{
    internal class DateOnlySerializer : StructSerializerBase<DateOnly>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateOnly value) =>
            context.Writer.WriteString(value.SerializeAsString());

        public override DateOnly Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) =>
            DateOnly.Parse(context.Reader.ReadString());
    }
}
