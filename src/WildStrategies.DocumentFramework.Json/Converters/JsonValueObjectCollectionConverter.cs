using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WildStrategies.DocumentFramework
{
    internal sealed class JsonValueObjectCollectionConverter<TValueObject> : JsonConverter<ValueObjectCollection<TValueObject>>
        where TValueObject : ValueObject
    {
        public override ValueObjectCollection<TValueObject> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            IEnumerable? values = (IEnumerable?)JsonSerializer.Deserialize(
                ref reader,
                typeof(List<>).MakeGenericType(typeToConvert.GetGenericTypeArgument<ValueObject>() ?? throw new Exception()),
                options
            );

            ValueObjectCollection<TValueObject> output = (ValueObjectCollection<TValueObject>)
                (Activator.CreateInstance(typeof(ValueObjectCollection<TValueObject>)) ?? throw new Exception());

            if (values != null)
            {
                foreach (object value in values)
                {
                    output.Add(value as TValueObject ?? throw new Exception());
                }
            }

            return output;
        }

        public override void Write(Utf8JsonWriter writer, ValueObjectCollection<TValueObject> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (IReadOnlyCollection<TValueObject>)value, options);
        }
    }
}
