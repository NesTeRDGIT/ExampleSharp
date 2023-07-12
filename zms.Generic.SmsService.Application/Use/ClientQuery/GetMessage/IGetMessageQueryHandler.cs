using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetMessage
{
    /// <summary>
    /// Обработчик запроса сообщения
    /// </summary>
    public interface IGetMessageQueryHandler : IQueryHandler<GetMessageQuery, GetMessageResponse>
    {
    }
}
