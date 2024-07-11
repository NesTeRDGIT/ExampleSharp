using System.Text.Json.Serialization;

namespace zms.Infrastructure.External.SmsService.Rostelecom.Api.Request.Common
{
    /// <summary>
    /// Базовый класс ответа на запрос
    /// </summary>
    public abstract class ResponseBase
    {
        /// <summary>
        /// Текстовое описание ошибки
        /// </summary>
        [JsonPropertyName("reason")]
        public string Reason { get; set; } = "";

        /// <summary>
        /// Описание статуса выполнения запроса
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = "";

        
    }
}
