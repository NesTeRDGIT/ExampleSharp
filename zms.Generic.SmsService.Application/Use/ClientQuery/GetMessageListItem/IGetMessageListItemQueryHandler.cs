using zms.Common.Application.Base.Cqrs.OfCollectionQuery;

namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetMessageListItem
{
    /// <summary>
    /// Обработчик запроса списка сообщений
    /// </summary>
    public interface IGetMessageListItemQueryHandler : ICollectionQueryHandler<GetMessageListItemQuery, MessageProjection>;
}
