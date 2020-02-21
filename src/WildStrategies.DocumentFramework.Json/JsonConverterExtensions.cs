using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace WildStrategies.DocumentFramework
{
    internal static class JsonConverterExtensions
    {
        public static PropertyInfo[] GetSerializableProperties(this Type type)
        {
            return type.GetProperties();
        }

        public static Dictionary<string, object> ReadValues(this ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            System.Reflection.PropertyInfo[] properties = typeToConvert.GetSerializableProperties();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException($"Unexpected JsonToken {reader.TokenType}");
                }

                string propertyName = reader.GetString();
                System.Reflection.PropertyInfo property = properties.FirstOrDefault(x => x.Name.Equals(propertyName, StringComparison.InvariantCulture));

                if (property == null)
                {
                    throw new JsonException($"Unexpected Propertye {propertyName}");
                }

                values.Add(propertyName, JsonSerializer.Deserialize(ref reader, property.PropertyType, options));

            }

            return values;
        }

        public static IDocumentFrameworkObject DeserializeFrameworkObject(this ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var values = reader.ReadValues(typeToConvert, options);

            IDocumentFrameworkObject output = (IDocumentFrameworkObject)Activator.CreateInstance(typeToConvert);
            foreach (System.Reflection.PropertyInfo property in typeToConvert.GetSerializableProperties().Where(x =>
                    x.CanWrite
                    && values.ContainsKey(x.Name)
                )
            )
            {
                property.SetValue(output, values[property.Name]);
            }

            return output;

        }

        public static void SerializeFrameworkObject(this Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            foreach (System.Reflection.PropertyInfo prop in value.GetType().GetSerializableProperties())
            {
                object propertyValue = prop.GetValue(value);
                if (propertyValue != null)
                {
                    writer.WritePropertyName(prop.Name);
                    JsonSerializer.Serialize(
                        writer,
                        propertyValue,
                        options
                    );
                }
            }

            writer.WriteEndObject();

        }
    }
}
