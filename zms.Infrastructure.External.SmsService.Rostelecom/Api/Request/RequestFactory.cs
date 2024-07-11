using zms.Infrastructure.External.SmsService.Rostelecom.Api.Request.GetStatus;
using zms.Infrastructure.External.SmsService.Rostelecom.Api.Request.SendSms;

namespace zms.Infrastructure.External.SmsService.Rostelecom.Api.Request
{
    /// <summary>
    /// Фабрика запросов
    /// </summary>
    public class RequestFactory
    {
        private readonly RostelecomOptions options;

        public RequestFactory(RostelecomOptions options)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        /// Получить запрос на проверку статуса сообщений
        /// </summary>
        /// <param name="id">Идентификаторы сообщений</param>
        /// <returns></returns>
        public GetStatusRequest GetStatusRequest(string id) => new(new SmsInfo(id));

        /// <summary>
        /// Получить запрос на проверку статуса сообщений
        /// </summary>
        /// <param name="sms">Сообщения</param>
        /// <returns></returns>
        public SendSmsRequest GetSendSmsRequest(Sms sms) => new(options.Sender, sms.Text, sms.Target);
    }
}
