using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace zms.Infrastructure.External.SmsService.Beeline.Api.Json.Converter
{
    /// <summary>
    /// Конвектор DateTime
    /// </summary>
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        private const string format = "dd.MM.yyyy HH:mm:ss";
        private const string format2 = "dd.MM.yy HH:mm:ss";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Не удалось преобразовать пустую строку в DateTime");
            return DateTime.TryParseExact(value, format, null, DateTimeStyles.None, out var result) ? result : DateTime.ParseExact(value, format2, null, DateTimeStyles.None);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("dd.MM.yyyy HH:mm:ss"));
        }
    }
}
