using zms.Generic.SmsService.Application.Outside.SmsSender;
using zms.Generic.SmsService.Domain.OfMessage;
using zms.Infrastructure.External.SmsService.Rostelecom.Api;
using zms.Infrastructure.External.SmsService.Rostelecom.Api.Request;
using zms.Infrastructure.External.SmsService.Rostelecom.Api.Request.SendSms;

namespace zms.Infrastructure.External.SmsService.Rostelecom
{
    public class SmsSender : ISmsSender
    {
        private readonly RostelecomService rostelecomService;
        private readonly RequestFactory requestFactory;

        public SmsSender(RostelecomService rostelecomService, RequestFactory requestFactory)
        {
            this.rostelecomService = rostelecomService ?? throw new ArgumentNullException(nameof(rostelecomService));
            this.requestFactory = requestFactory ?? throw new ArgumentNullException(nameof(requestFactory));
        }

        public Task ConnectAsync()
        {
            return Task.CompletedTask;
        }

        public Task DisconnectAsync()
        {
            return Task.CompletedTask;
        }

        public async Task<SendMessageResult> SendAsync(Message message)
        {
            var sms = new Sms(message.Text, message.Recipient.Phone.Value);
            var response = await rostelecomService.SendMessageAsync(requestFactory.GetSendSmsRequest(sms));
            return response.Status != "ok" ? 
                new SendMessageResult(SendResultEnum.Error, response.Reason, response.MessageInfo.MessageId, Constants.ProviderName) : 
                new SendMessageResult(SendResultEnum.Success, "Сообщение отправлено на сервер", response.MessageInfo.MessageId, Constants.ProviderName);
        }
    }
}