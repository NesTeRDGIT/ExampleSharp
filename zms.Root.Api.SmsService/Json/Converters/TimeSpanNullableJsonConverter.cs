using System.Text.Json;
using System.Text.Json.Serialization;

namespace zms.Root.Api.SmsService.Json.Converters
{
    public class TimeSpanNullableJsonConverter : JsonConverter<TimeSpan?>
    {
        private const string format = "HH:mm:ss";

        public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (string.IsNullOrEmpty(value))
                return null;
            return TimeSpan.Parse(value);
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteStringValue(value.Value.ToString(format));
            }
        }
    }
}
