using System.Text.Json.Serialization;
using zms.Common.SharedKernel.Exception;

namespace zms.Infrastructure.External.SmsService.Beeline.Api.Request.SendSms
{
    /// <summary>
    /// СМС
    /// </summary>
    public class Sms
    {
        public Sms(string sender, string message, string target, string timePeriod)
        {
            if (string.IsNullOrEmpty(sender))
            {
                throw new BadValueDomainException("Отправитель не может быть пустым");
            }

            TimePeriod = string.IsNullOrEmpty(sender) ? "00:00-00:00" : timePeriod;
            Sender = string.IsNullOrEmpty(sender) ? throw new BadValueDomainException("Отправитель не может быть пустым") : sender;
            Message = string.IsNullOrEmpty(message) ? throw new BadValueDomainException("Текст сообщения не может быть пустым") : message;
            Target = string.IsNullOrEmpty(target) ? throw new BadValueDomainException("Получатель не может быть пустым") : target;
        }

        /// <summary>
        /// Пользовательский ID рассылки (необязательный параметр). Возвращается обратно в неизменном виде.
        /// </summary>
        [JsonPropertyName("post_id")]
        [JsonPropertyOrder(1)]
        public long Id { get; set; }

        /// <summary>
        /// Отправитель
        /// </summary>
        [JsonPropertyName("sender")]
        [JsonPropertyOrder(2)]
        public string Sender { get; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        [JsonPropertyName("message")]
        [JsonPropertyOrder(3)]
        public string Message { get; }

        /// <summary>
        /// Получатель
        /// </summary>
        [JsonPropertyName("target")]
        [JsonPropertyOrder(4)]
        public string Target { get; }


        /// <summary>
        /// Период отправки сообщения в формате hh:mm-hh:mm (час:мин-час:мин), в течение которого сообщение должно быть доставлено получателям.
        /// Опция позволяет запретить доставку сообщений, например, в ночное время.
        /// Для указанного периода времени можно уточнить часовой пояс в параметре TimeZone.
        /// </summary>
        [JsonPropertyName("time_period")]
        [JsonPropertyOrder(5)]

        public string TimePeriod { get; }

        /// <summary>
        /// Отправитель
        /// </summary>
        [JsonPropertyName("time_local")]
        [JsonPropertyOrder(6)]
        public string TimeZone => "1";
    }
}
