using System.Text.Json.Serialization;

namespace zms.Infrastructure.External.SmsService.Rostelecom.Api.Request.GetStatus
{
    /// <summary>
    /// Информация о статусе СМС
    /// </summary>
    public class StatusInfo
    {
        /// <summary>
        /// Дата и время создания
        /// </summary>
        [JsonPropertyName("created_at")]
        [JsonPropertyOrder(1)]
        public DateTime Created { get; set; }

        /// <summary>
        /// Номер получателя
        /// </summary>
        [JsonPropertyName("msisdn")]
        [JsonPropertyOrder(2)]
        public long Target { get; set; }

        /// <summary>
        /// Количество частей в сообщении
        /// </summary>
        [JsonPropertyName("pdu_count")]
        [JsonPropertyOrder(3)]
        public int? SmsCount { get; set; }

        /// <summary>
        /// Статус сообщения
        /// </summary>
        [JsonPropertyName("status")]
        [JsonPropertyOrder(4)]
        public string Status { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        [JsonPropertyName("text")]
        [JsonPropertyOrder(5)]
        public string Text { get; set; }

        /// <summary>
        /// Идентификатор сообщения
        /// </summary>
        [JsonPropertyName("uid")]
        [JsonPropertyOrder(6)]
        public string MessageId { get; set; }

        /// <summary>
        /// Дата и время обновления
        /// </summary>
        [JsonPropertyName("updated_at")]
        [JsonPropertyOrder(7)]
        public DateTime Updated { get; set; }

        /// <summary>
        /// Ошибка
        /// </summary>
        [JsonPropertyName("error_code")]
        [JsonPropertyOrder(8)]
        public int Error { get; set; }
    }
}
