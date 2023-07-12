using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetMessage
{
    /// <summary>
    /// Запрос сообщения
    /// </summary>
    public class GetMessageQuery : IQuery<GetMessageResponse>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }
    }
}
