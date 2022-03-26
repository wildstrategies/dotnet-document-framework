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

        public static Dictionary<string, object?> ReadValues(this ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Dictionary<string, object?> values = new Dictionary<string, object?>();
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

                string? propertyName = reader.GetString();
                if (propertyName != null)
                {
                    PropertyInfo? property = properties.FirstOrDefault(x => x.Name.Equals(propertyName, StringComparison.InvariantCulture));

                    if (property == null)
                    {
                        throw new JsonException($"Unexpected Property {propertyName}");
                    }

                    values.Add(propertyName, JsonSerializer.Deserialize(ref reader, property.PropertyType, options));
                }

            }

            return values;
        }

        public static IDocumentFrameworkObject DeserializeFrameworkObject(this ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Dictionary<string, object> values = reader.ReadValues(typeToConvert, options);

            IDocumentFrameworkObject output = (IDocumentFrameworkObject)(Activator.CreateInstance(typeToConvert) ?? throw new Exception());
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
                object? propertyValue = prop.GetValue(value);
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

        public static Type? GetGenericTypeArgument<TArgument>(this Type typeToConvert)
        {
            if (typeToConvert.IsGenericType)
            {
                Type[] genericTypes = typeToConvert.GetGenericArguments();
                if (genericTypes.Length == 1 && genericTypes[0].IsSubclassOf(typeof(TArgument)))
                {
                    return genericTypes[0];
                }
            }

            return null;
        }

        public static Type? GetGenericType<TArgument>(this Type typeToConvert, Type genericType)
        {
            Type objectType = GetGenericTypeArgument<TArgument>(typeToConvert);
            if (objectType != null)
            {
                return genericType.MakeGenericType(objectType);
            }

            return null;
        }
    }
}
