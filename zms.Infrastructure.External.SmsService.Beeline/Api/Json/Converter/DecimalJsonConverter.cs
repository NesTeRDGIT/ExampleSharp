using System.Text.Json.Serialization;
using System.Text.Json;

namespace zms.Infrastructure.External.SmsService.Beeline.Api.Json.Converter
{
    /// <summary>
    /// Конвектор decimal
    /// </summary>
    public class DecimalJsonConverter : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return Convert.ToDecimal(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }
}
