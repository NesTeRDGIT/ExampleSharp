using zms.Common.SharedKernel.Exception;
using zms.Generic.SmsService.Application.Outside.SmsStatusChecker;
using zms.Generic.SmsService.Domain.OfMessage;
using zms.Infrastructure.External.SmsService.Beeline.Api;
using zms.Infrastructure.External.SmsService.Beeline.Api.Request;

namespace zms.Infrastructure.External.SmsService.Beeline
{
    public class SmsStatusChecker : ISmsStatusChecker
    {
        private readonly BeelineService beelineService;
        private readonly RequestFactory requestFactory;

        public SmsStatusChecker(BeelineService beelineService, RequestFactory requestFactory)
        {
            this.beelineService = beelineService ?? throw new ArgumentNullException(nameof(beelineService));
            this.requestFactory = requestFactory ?? throw new ArgumentNullException(nameof(requestFactory));
        }

        public Task ConnectAsync()
        {
            return Task.CompletedTask;
        }

        public async Task<StatusMessageResult> GetStatusAsync(Message message)
        {
            if (message.Provider.Name != Constants.ProviderName)
            {
                throw new InvalidOperationDomainException(
                    $"Имя провайдера({message.Provider.Name}) для сообщения({message.Id.Value}) отличается от проверяющего провайдера({Constants.ProviderName})");
            }
            var response = await beelineService.GetStatusAsync(requestFactory.GetStatusRequest(message.ExternalId.Value));
            var result = response.StatusInfo.FirstOrDefault();
            if (result == null)
            {
                throw new Exception("Сервер не вернул статус сообщения");
            }

            return result.StatusCode switch
            {
                "queued" => new StatusMessageResult(StatusResultEnum.None, result.CloseTime?.AddHours(1), "Сообщение находится в очереди отправки и еще не было передано оператору", result.SmsCount),
                "accepted" => new StatusMessageResult(StatusResultEnum.None, result.CloseTime?.AddHours(1), "Сообщение уже передано оператору",result.SmsCount),
                "delivered" => new StatusMessageResult(StatusResultEnum.Success, result.CloseTime?.AddHours(1), "Сообщение успешно доставлено абоненту", result.SmsCount),
                "rejected" => new StatusMessageResult(StatusResultEnum.Error, result.CloseTime?.AddHours(1), "Сообщение отклонено оператором", result.SmsCount),
                "undeliverable" => new StatusMessageResult(StatusResultEnum.Error, result.CloseTime?.AddHours(1), "Сообщение невозможно доставить из-за недоступности абонента", result.SmsCount),
                "error" => new StatusMessageResult(StatusResultEnum.Error, result.CloseTime?.AddHours(1), $"Ошибка отправки: {result.Status}", result.SmsCount),
                "expired" => new StatusMessageResult(StatusResultEnum.Error, result.CloseTime?.AddHours(1), "Истекло время ожидания финального статуса", result.SmsCount),
                "unknown" => new StatusMessageResult(StatusResultEnum.Error, result.CloseTime?.AddHours(1), "Статус сообщения неизвестен", result.SmsCount),
                "aborted" => new StatusMessageResult(StatusResultEnum.Error, result.CloseTime?.AddHours(1), "Сообщение отменено пользователем", result.SmsCount),
                _ => new StatusMessageResult(StatusResultEnum.Error, result.CloseTime?.AddHours(1), $"Ошибка отправки: Неизвестный тип статуса {result.StatusCode}: {result.Status}", result.SmsCount)
            };
        }

        public Task DisconnectAsync()
        {
            return Task.CompletedTask;
        }
    }
}
