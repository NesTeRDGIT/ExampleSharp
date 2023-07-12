using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetStatus
{
    /// <summary>
    /// Обработчик запроса статусов сообщений
    /// </summary>
    public interface IGetStatusQueryHandler : IQueryHandler<GetStatusQuery, GetStatusResponse>
    {
    }
}
