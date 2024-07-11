using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetSentMessagesReport
{
    /// <summary>
    /// Обработчик запроса получения отчета отправленных сообщений
    /// </summary>
    public interface IGetSentMessagesReportQueryHandler : IQueryHandler<GetSentMessagesReportQuery, GetSentMessagesReportResponse>
    {
    }
}
