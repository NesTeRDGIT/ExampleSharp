using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetMessageListItem
{
    /// <summary>
    /// Обработчик запроса списка сообщений
    /// </summary>
    public interface IGetMessageListItemQueryHandler : IQueryHandler<GetMessageListItemQuery, GetMessageListItemResponse>
    {
    }
}
