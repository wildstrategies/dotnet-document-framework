using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WildStrategies.DocumentFramework
{

    internal class JsonDocumentConverterFactory : JsonConverterFactory
    {
        private static Type GetEntityType(Type typeToConvert)
        {
            if (typeToConvert.IsGenericType)
            {
                Type[] genericTypes = typeToConvert.GetGenericArguments();
                if (genericTypes.Length == 1 && genericTypes[0].IsSubclassOf(typeof(Entity)))
                {
                    return genericTypes[0];
                }
            }

            return null;
        }

        private static Type GetGenericType(Type typeToConvert)
        {
            Type entityType = GetEntityType(typeToConvert);
            if (entityType != null)
            {
                return typeof(Document<>).MakeGenericType(entityType);
            }

            return null;
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return GetGenericType(typeToConvert)?.Equals(typeToConvert) ?? false;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return (JsonConverter)Activator.CreateInstance(
                typeof(JsonDocumentConverter<>).MakeGenericType(GetEntityType(typeToConvert))
            );
        }
    }
}
