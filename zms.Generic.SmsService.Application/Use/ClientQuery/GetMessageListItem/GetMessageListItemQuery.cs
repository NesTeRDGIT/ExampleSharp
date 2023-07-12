using zms.Common.Application.Base.Cqrs;
using zms.Common.Application.LightRead;

namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetMessageListItem
{
    /// <summary>
    /// Запрос списка сообщений
    /// </summary>
    public class GetMessageListItemQuery : IQuery<GetMessageListItemResponse>
    {
        /// <summary>
        /// Параметры чтения
        /// </summary>
        public LightReadParams<MessageProjection> LightReadParams { get; set; }
    }
}
