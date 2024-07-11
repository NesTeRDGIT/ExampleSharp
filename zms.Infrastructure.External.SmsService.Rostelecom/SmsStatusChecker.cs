using zms.Common.SharedKernel.Exception;
using zms.Generic.SmsService.Application.Outside.SmsStatusChecker;
using zms.Generic.SmsService.Domain.OfMessage;
using zms.Infrastructure.External.SmsService.Rostelecom.Api;
using zms.Infrastructure.External.SmsService.Rostelecom.Api.Request;

namespace zms.Infrastructure.External.SmsService.Rostelecom
{
    public class SmsStatusChecker : ISmsStatusChecker
    {
        private readonly RostelecomService rostelecomService;
        private readonly RequestFactory requestFactory;

        public SmsStatusChecker(RostelecomService rostelecomService, RequestFactory requestFactory)
        {
            this.rostelecomService = rostelecomService ?? throw new ArgumentNullException(nameof(rostelecomService));
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

            var response = await rostelecomService.GetStatusAsync(requestFactory.GetStatusRequest(message.ExternalId.Value));
            var info = response.StatusInfo;

            return info.Status switch
            {
                "delivered" =>  new StatusMessageResult(StatusResultEnum.Success, info.Updated, "Сообщение успешно доставлено абоненту", info.SmsCount ?? 0),
                "awaiting_report" =>  new StatusMessageResult(StatusResultEnum.None, info.Updated, "Сообщение принято оператором, но еще не был получен статус доставки", info.SmsCount ?? 0),
                "insufficient_balance" => new StatusMessageResult(StatusResultEnum.Error, info.Updated, "Сообщение не отправлено, так как баланса клиента недостаточно", info.SmsCount ?? 0),
                "failed" =>  new StatusMessageResult(StatusResultEnum.Error, info.Updated, $"Сообщение не доставлено: {info.Error}", info.SmsCount ?? 0),
                "rejected" =>  new StatusMessageResult(StatusResultEnum.Error, info.Updated, $"Сообщение отклонено для отправки: {info.Error}", info.SmsCount ?? 0),
                _ => new StatusMessageResult(StatusResultEnum.Error, info.Updated, $"Ошибка отправки: Неизвестный тип статуса {info.Status}: {info.Error}", info.SmsCount ?? 0)
            };
        }

        public Task DisconnectAsync()
        {
            return Task.CompletedTask;
        }
    }
}
