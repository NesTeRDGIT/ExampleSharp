using zms.Common.Application.Base.Cqrs;
using zms.Common.Application.LightRead;
using zms.Generic.SmsService.Application.Use.ClientQuery.GetMessageListItem;

namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetMessageSummary
{
    /// <summary>
    /// Запрос краткой информации о сообщениях
    /// </summary>
    public class GetMessageSummaryQuery : IQuery<GetMessageSummaryResponse>
    {
        /// <summary>
        /// Параметры чтения
        /// </summary>
        public LightReadParams<MessageProjection> LightReadParams { get; set; }
    }
}
