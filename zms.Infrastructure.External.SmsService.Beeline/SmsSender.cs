using zms.Generic.SmsService.Application.Outside.SmsSender;
using zms.Generic.SmsService.Domain.OfMessage;
using zms.Infrastructure.External.SmsService.Beeline.Api;
using zms.Infrastructure.External.SmsService.Beeline.Api.Request;

namespace zms.Infrastructure.External.SmsService.Beeline
{
    public class SmsSender : ISmsSender
    {
        private readonly BeelineOptions options;
        private readonly BeelineService beelineService;
        private readonly RequestFactory requestFactory;

        public SmsSender(BeelineOptions options, BeelineService beelineService, RequestFactory requestFactory)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.beelineService = beelineService ?? throw new ArgumentNullException(nameof(beelineService));
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
            var sms = new SmsProjection
            {
                Message = message.Text,
                Sender = options.Sender,
                Target = message.Recipient.Phone.Value,
                SendingPeriod = $"{message.SendingPeriod.TimeStart.Hours:D2}:{message.SendingPeriod.TimeStart.Minutes:D2}-{message.SendingPeriod.TimeEnd.Hours:D2}:{message.SendingPeriod.TimeEnd.Minutes:D2}"
            };
            var response = await beelineService.SendMessageAsync(requestFactory.GetSendSmsRequest(sms));
            var result = response.StatusInfo.FirstOrDefault();
            if (result == null)
            {
                throw new Exception("После отправки СМС сервер не вернул статус");
            }
            return new SendMessageResult(SendResultEnum.Success, "Сообщение отправлено на сервер", result.SmsGroupId, Constants.ProviderName);
        }
    }
}