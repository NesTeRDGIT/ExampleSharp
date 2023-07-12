using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.Query.GetMessage
{
    /// <summary>
    /// Обработчик запроса сообщений
    /// </summary>
    public interface IGetMessageQueryHandler : IQueryHandler<GetMessageQuery, GetMessageResponse>
    {
    }
}
