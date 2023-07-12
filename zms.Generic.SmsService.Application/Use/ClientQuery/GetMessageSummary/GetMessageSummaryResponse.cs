using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetMessageSummary
{
    /// <summary>
    /// Ответ на запрос краткой информации о сообщениях
    /// </summary>
    public class GetMessageSummaryResponse : IResponse
    {
        /// <summary>
        /// Краткая информация о пакетах обмена
        /// </summary>
        public MessageSummaryData Summary { get; set; }
    }
}
