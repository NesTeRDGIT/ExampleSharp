using zms.Common.Application.Base.Cqrs;
using zms.Generic.SmsService.Application.Use.ClientQuery.GetMessage.Projections;

namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetMessage
{
    /// <summary>
    /// Ответ на запрос сообщения
    /// </summary>
    public class GetMessageResponse : IResponse
    {
        /// <summary>
        /// Письмо
        /// </summary>
        public MessageProjection Message { get; set; }
    }
}
