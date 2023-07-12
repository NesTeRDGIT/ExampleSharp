using zms.Generic.SmsService.Application.Use.ClientQuery.GetMessage.Projections;
using zms.Generic.SmsService.Domain.OfMessage;

namespace zms.Generic.SmsService.Application.Persistence.QueryObject.ClientQuery
{
    /// <summary>
    /// Объект запроса сообщений
    /// </summary>
    public interface IGetMessageQueryObject
    {
        /// <summary>
        /// Получить
        /// </summary>
        /// <param name="id">Идентификатор сообщения</param>
        /// <returns></returns>
        public Task<MessageProjection> GetAsync(MessageId id);
    }
}
