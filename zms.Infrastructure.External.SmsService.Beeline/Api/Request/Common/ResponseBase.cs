using System.Text.Json.Serialization;

namespace zms.Infrastructure.External.SmsService.Beeline.Api.Request.Common
{
    /// <summary>
    /// Базовый класс ответа на запрос
    /// </summary>
    public abstract class ResponseBase
    {
        /// <summary>
        /// Идентификатор ответа
        /// </summary>
        [JsonPropertyName("agt_id")]
        [JsonPropertyOrder(-2)]
        public string Id { get; set; }

        /// <summary>
        /// Дата ответа
        /// </summary>
        [JsonPropertyName("date_report")]
        [JsonPropertyOrder(-1)]
        public DateTime Date { get; set; }
    }
}
