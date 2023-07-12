using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.Query.GetMessage
{
    /// <summary>
    /// Ответ на запрос сообщений
    /// </summary>
    public class GetMessageResponse : IResponse
    {
        /// <summary>
        /// Сообщения
        /// </summary>
        public IList<MessageProjection> Messages { get; set; }
    }
}
