using zms.Common.SharedKernel.Exception;
using zms.Generic.SmsService.Application.Persistence.QueryObject.ClientQuery;
using zms.Generic.SmsService.Application.Use.ClientQuery.GetMessage;
using zms.Generic.SmsService.Domain.OfMessage;

namespace zms.Generic.SmsService.Application.Interactor.ClientQuery.GetMessage
{
    public class GetMessageQueryHandler : IGetMessageQueryHandler
    {
        private readonly IGetMessageQueryObject getMessageQueryObject;

        public GetMessageQueryHandler(IGetMessageQueryObject getMessageQueryObject)
        {
            this.getMessageQueryObject = getMessageQueryObject ?? throw new ArgumentNullException(nameof(getMessageQueryObject));
        }

        public async Task<GetMessageResponse> HandleAsync(GetMessageQuery query)
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));
            var messageId = new MessageId(query.Id);
            var message = await getMessageQueryObject.GetAsync(messageId);
            if (message == null)
            {
                throw new EntityNotExistDomainException($"Не удалось найти сообщение с кодом: {query.Id}");
            }
            return new GetMessageResponse
            {
                Message = message
            };
        }
    }
}
