using zms.Generic.SmsService.Application.Outside.SmsSender;
using zms.Generic.SmsService.Domain.OfMessage;

namespace zms.Root.Worker.SmsService.TestMode
{
    internal class TestSmsSender : ISmsSender
    {
        public Task ConnectAsync()
        {
            return Task.CompletedTask;
        }

        public Task<SendMessageResult> SendAsync(Message message)
        {
            return message.Text switch
            {
                "SendError" => Task.FromResult(new SendMessageResult(SendResultEnum.Error, "Ошибка отправки СМС провайдеру(вызванная пользователем)", "", "SmsTestProvider")),
                _ => Task.FromResult(new SendMessageResult(SendResultEnum.Success, "Успешная тестовая отправка SMS", "", "SmsTestProvider"))
            };
        }

        public Task DisconnectAsync()
        {
            return Task.CompletedTask;
        }
    }
}
