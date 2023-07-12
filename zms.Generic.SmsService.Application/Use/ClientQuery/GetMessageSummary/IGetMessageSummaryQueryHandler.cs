using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetMessageSummary
{
    /// <summary>
    /// Обработчик запроса краткой информации о сообщениях
    /// </summary>
    public interface IGetMessageSummaryQueryHandler : IQueryHandler<GetMessageSummaryQuery, GetMessageSummaryResponse>
    {
    }
}
