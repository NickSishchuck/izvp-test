using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using TestApi.Interfaces;

namespace TestApi.Implementations
{
    /// <summary>
    /// Provides JSON serialization and deserialization functionality.
    /// </summary>
    public class JsonSerializer : IJsonSerializer
    {
        /// <summary>
        /// The JSON serializer options.
        /// </summary>
        private readonly JsonSerializerOptions _options = new()
        {
            // Minified output
            WriteIndented = false,

            // Use camelCase for property names
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,

            // Use camelCase for dictionary keys
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,

            // Case insensitive property matching
            PropertyNameCaseInsensitive = true,

            // Convert enums to/from strings
            Converters = { new JsonStringEnumConverter() },

            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        /// <inheritdoc />
        public T? FromJson<T>(string json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(json, _options);
        }

        /// <inheritdoc />
        public string ToJson<T>(T obj)
        {
            return System.Text.Json.JsonSerializer.Serialize(obj, _options);
        }
    }
}
