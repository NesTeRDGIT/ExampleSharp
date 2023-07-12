using zms.Common.Application.Base.Cqrs.OfPaginationCollectionResponse;

namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetMessageListItem
{
    /// <summary>
    /// Ответ на запрос списка сообщений
    /// </summary>
    public class GetMessageListItemResponse : PaginationCollectionResponse<MessageProjection>
    {
    }
}
