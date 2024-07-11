using zms.Infrastructure.External.SmsService.Beeline.Api.Request.GetStatus;
using zms.Infrastructure.External.SmsService.Beeline.Api.Request.SendSms;

namespace zms.Infrastructure.External.SmsService.Beeline.Api.Request
{
    /// <summary>
    /// Фабрика запросов
    /// </summary>
    public class RequestFactory
    {
        private readonly BeelineOptions options;

        public RequestFactory(BeelineOptions options)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        /// Получить запрос на проверку статуса сообщений
        /// </summary>
        /// <param name="id">Идентификаторы сообщений</param>
        /// <returns></returns>
        public GetStatusRequest GetStatusRequest(params string[] id) => new(options.UserName, options.UserPassword, id.Select(x => new SmsInfo(x)));

        /// <summary>
        /// Получить запрос на проверку статуса сообщений
        /// </summary>
        /// <param name="sms">Сообщения</param>
        /// <returns></returns>
        public SendSmsRequest GetSendSmsRequest(params SmsProjection[] sms) => new(options.UserName, options.UserPassword, sms.Select(x => new Sms(x.Sender, x.Message, x.Target, x.SendingPeriod)));
    }
}
