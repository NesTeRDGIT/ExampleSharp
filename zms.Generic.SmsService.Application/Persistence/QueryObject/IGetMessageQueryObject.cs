using zms.Generic.SmsService.Application.Use.Query.GetMessage;
using zms.Generic.SmsService.Domain.OfMessage;

namespace zms.Generic.SmsService.Application.Persistence.QueryObject
{
    /// <summary>
    /// Объект запроса сообщений
    /// </summary>
    public interface IGetMessageQueryObject
    {
        /// <summary>
        /// Получить
        /// </summary>
        /// <param name="id">Идентификаторы сообщений</param>
        /// <returns></returns>
        public Task<IList<MessageProjection>> GetAsync(IList<MessageId> id);
    }
}
