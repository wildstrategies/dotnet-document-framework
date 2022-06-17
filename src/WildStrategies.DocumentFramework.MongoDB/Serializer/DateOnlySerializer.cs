using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
