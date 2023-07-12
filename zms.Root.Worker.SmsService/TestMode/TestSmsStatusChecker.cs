using zms.Generic.SmsService.Application.Outside.SmsStatusChecker;
using zms.Generic.SmsService.Domain.OfMessage;

namespace zms.Root.Worker.SmsService.TestMode
{
    internal class TestSmsStatusChecker : ISmsStatusChecker
    {
        public Task ConnectAsync()
        {
            return Task.CompletedTask;
        }

        public Task<StatusMessageResult> GetStatusAsync(Message message)
        {
            return message.Text switch
            {
                "ErrorSms" => Task.FromResult(new StatusMessageResult(StatusResultEnum.Error, DateTime.Now,  "Ошибка отправки SMS провайдером(вызванная пользователем)", 0)),
                _ => Task.FromResult(new StatusMessageResult(StatusResultEnum.Success, DateTime.Now, "Успешная тестовая отправка SMS", 2))
            };
        }

        public Task DisconnectAsync()
        {
            return Task.CompletedTask;
        }
    }
}
