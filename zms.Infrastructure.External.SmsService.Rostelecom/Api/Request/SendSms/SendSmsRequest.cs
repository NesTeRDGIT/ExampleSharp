using System.Text.Json.Serialization;
using zms.Infrastructure.External.SmsService.Rostelecom.Api.Request.Common;

namespace zms.Infrastructure.External.SmsService.Rostelecom.Api.Request.SendSms
{
    /// <summary>
    /// Запрос отправки СМС
    /// </summary>
    public class SendSmsRequest : RequestBase
    {
        public SendSmsRequest(string sender, string text, string target)
        {
            Sender = !string.IsNullOrEmpty(sender) ? sender  : throw new ArgumentNullException(nameof(sender));
            Text = !string.IsNullOrEmpty(text) ? text : throw new ArgumentNullException(nameof(text));
            Target = !string.IsNullOrEmpty(target) ? target : throw new ArgumentNullException(nameof(target));
        }

        /// <summary>
        /// Отправитель
        /// </summary>
        [JsonPropertyName("shortcode")]
        [JsonPropertyOrder(2)]
        public string Sender { get; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        [JsonPropertyName("text")]
        [JsonPropertyOrder(3)]
        public string Text { get; }

        /// <summary>
        /// Получатель
        /// </summary>
        [JsonPropertyName("msisdn")]
        [JsonPropertyOrder(1)]
        public string Target { get; }
    }
}
