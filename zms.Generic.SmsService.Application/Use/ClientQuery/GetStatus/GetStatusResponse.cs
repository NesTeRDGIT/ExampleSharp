using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetStatus
{
    /// <summary>
    /// Ответ на запрос статусов сообщений
    /// </summary>
    public class GetStatusResponse : IResponse
    {
        /// <summary>
        /// Статусы
        /// </summary>
        public IList<StatusProjection> Statuses { get; set; }
    }
}
