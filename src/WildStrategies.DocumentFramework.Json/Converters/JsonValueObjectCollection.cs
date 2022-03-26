using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WildStrategies.DocumentFramework
{
    internal class JsonValueObjectCollectionConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.GetGenericType<ValueObject>(typeof(ValueObjectCollection<>))?.Equals(typeToConvert) ?? false;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            Type type = typeToConvert.GetGenericTypeArgument<ValueObject>() ?? throw new ArgumentNullException(nameof(typeToConvert));
            type = typeof(JsonValueObjectCollectionConverter<>).MakeGenericType(type);

            return (JsonConverter)(Activator.CreateInstance(type) ?? throw new ArgumentNullException(nameof(type)));
        }
    }

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
