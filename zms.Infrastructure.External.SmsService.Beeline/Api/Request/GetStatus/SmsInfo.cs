using System.Text.Json.Serialization;

namespace zms.Infrastructure.External.SmsService.Beeline.Api.Request.GetStatus
{
    /// <summary>
    /// Информация о СМС
    /// </summary>
    public class SmsInfo
    {
        public SmsInfo(string smsGroupId)
        {
            SmsGroupId = smsGroupId;
        }

        /// <summary>
        /// Действие
        /// </summary>
        [JsonPropertyName("action")]
        [JsonPropertyOrder(1)]
        public string Action => "status";

        /// <summary>
        /// ID рассылки сообщений
        /// </summary>
        [JsonPropertyName("sms_group_id")]
        [JsonPropertyOrder(2)]
        public string SmsGroupId { get; }
    }
}
