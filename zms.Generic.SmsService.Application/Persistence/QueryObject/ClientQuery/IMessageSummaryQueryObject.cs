using zms.Common.Application.LightRead;
using zms.Generic.SmsService.Application.Use.ClientQuery.GetMessageListItem;
using zms.Generic.SmsService.Application.Use.ClientQuery.GetMessageSummary;

namespace zms.Generic.SmsService.Application.Persistence.QueryObject.ClientQuery
{
    /// <summary>
    /// Объект запроса краткой информации о сообщениях
    /// </summary>
    public interface IMessageSummaryQueryObject
    {
        /// <summary>
        /// Получить
        /// </summary>
        /// <param name="lightReadParams">Параметры чтения</param>
        /// <returns></returns>
        public Task<MessageSummaryData> GetAsync(LightReadParams<MessageProjection> lightReadParams);
    }
}
