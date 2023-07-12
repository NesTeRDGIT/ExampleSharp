using zms.Common.SharedKernel.Base.Domain;

namespace zms.Generic.SmsService.Domain.OfMessage
{
    /// <summary>
    /// Идентификатор сообщения
    /// </summary>
    public class MessageId : EntityId<MessageId>
    {
        public MessageId(long value)
        {
            Value = value;
        }
    }
}