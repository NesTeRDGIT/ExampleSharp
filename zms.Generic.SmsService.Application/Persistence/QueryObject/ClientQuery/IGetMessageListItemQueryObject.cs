using zms.Common.Application.LightRead;
using zms.Generic.SmsService.Application.Use.ClientQuery.GetMessageListItem;

namespace zms.Generic.SmsService.Application.Persistence.QueryObject.ClientQuery
{
    /// <summary>
    /// Объект запроса сообщений
    /// </summary>
    public interface IGetMessageListItemQueryObject
    {
        /// <summary>
        /// Получить элементы
        /// </summary>
        /// <param name="lightReadParams">Параметры чтения</param>
        Task<List<MessageProjection>> GetAsync(LightReadParams<MessageProjection> lightReadParams);

        /// <summary>
        /// Получить количество элементов
        /// </summary>
        /// <param name="lightReadParams">Параметры чтения</param>
        Task<long> CountAsync(LightReadParams<MessageProjection> lightReadParams);
    }
}
