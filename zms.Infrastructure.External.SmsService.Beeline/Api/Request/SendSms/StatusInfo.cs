using System.Text.Json.Serialization;

namespace zms.Infrastructure.External.SmsService.Beeline.Api.Request.SendSms
{
    /// <summary>
    /// Информация о статусе СМС
    /// </summary>
    public class SmsStatus
    {
        /// <summary>
        /// ID рассылки сообщений
        /// </summary>
        [JsonPropertyName("sms_group_id")]
        [JsonPropertyOrder(1)]
        public string SmsGroupId { get; set; }

        /// <summary>
        /// ID сообщения
        /// </summary>
        [JsonPropertyName("id")]
        [JsonPropertyOrder(2)]
        public string SmsId { get; set; }


        /// <summary>
        /// Тип сообщения
        /// </summary>
        [JsonPropertyName("sms_type")]
        [JsonPropertyOrder(3)]
        public string SmsType { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [JsonPropertyName("phone")]
        [JsonPropertyOrder(5)]
        public string Phone { get; set; }

        /// <summary>
        /// Кол-во единиц ресурсов на данное сообщение
        /// </summary>
        [JsonPropertyName("sms_res_count")]
        [JsonPropertyOrder(10)]
        public string SmsCount { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        [JsonPropertyName("message")]
        [JsonPropertyOrder(11)]
        public string Text { get; set; }


        /// <summary>
        /// Действие
        /// </summary>
        [JsonPropertyName("action")]
        [JsonPropertyOrder(17)]
        public string Action { get; set; }
    }
}
