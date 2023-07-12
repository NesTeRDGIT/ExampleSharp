using zms.Common.SharedKernel.Base.Specification;
using zms.Common.SharedKernel.Common.Dating;

namespace zms.Generic.SmsService.Domain.OfMessage
{
    /// <summary>
    /// Фабрика спецификаций для Message
    /// </summary>
    public class MessageSpecificationFactory
    {
        /// <summary>
        /// Условие по идентификатору
        /// </summary>
        /// <param name="messageId">Идентификатор</param>
        /// <returns></returns>
        public static Specification<Message> ById(MessageId messageId) => new AdHocSpecification<Message>(a => a.Id == messageId);

        /// <summary>
        /// Условие по статусу
        /// </summary>
        /// <param name="status">Статус</param>
        /// <returns></returns>
        public static Specification<Message> ByStatus(Status status) => new AdHocSpecification<Message>(a => a.Status.Value == status.Value);

        /// <summary>
        /// Период отправки включает время
        /// </summary>
        /// <param name="time">Время</param>
        /// <returns></returns>
        public static Specification<Message> TimePeriodInclude(Time time) => new AdHocSpecification<Message>(a => time >=a.SendingPeriod.TimeStart && time <= a.SendingPeriod.TimeEnd);
    }
}
