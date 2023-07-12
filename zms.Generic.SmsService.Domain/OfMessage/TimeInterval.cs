using zms.Common.SharedKernel.Base.Domain;
using zms.Common.SharedKernel.Common.Dating;
using zms.Common.SharedKernel.Exception;

namespace zms.Generic.SmsService.Domain.OfMessage
{
    /// <summary>
    /// Интервал времени
    /// </summary>
    public class TimeInterval : ValueObject<TimeInterval>
    {
        private TimeInterval()
        {

        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="timeStart">Время начала периода</param>
        /// <param name="timeEnd">Время окончания периода</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="BadValueDomainException">Время начала периода должна быть меньше, чем время окончания периода</exception>
        public TimeInterval(Time timeStart, Time timeEnd) : this()
        {
            TimeStart = timeStart ?? throw new ArgumentNullException(nameof(timeStart));
            TimeEnd = timeEnd ?? throw new ArgumentNullException(nameof(timeEnd));
            if (TimeStart > TimeEnd)
                throw new BadValueDomainException("Время начала периода должна быть меньше, чем время окончания периода");
        }

        /// <summary>
        /// Дата начала интервала
        /// </summary>
        public Time TimeStart { get; }

        /// <summary>
        /// Дата окончания интервала
        /// </summary>
        public Time TimeEnd { get; }

        /// <summary>
        /// Значение по умолчанию
        /// </summary>
        public static TimeInterval Default => new (Time.MinValue, Time.MaxValue);

        /// <summary>
        /// Включает ли период время
        /// </summary>
        /// <param name="time">Время</param>
        /// <returns></returns>
        public bool Includes(Time time)
        {
            return time >= TimeStart && time <= TimeEnd;
        }

        protected override int GetValueHashCode()
        {
            return HashCode.Combine(TimeStart, TimeEnd);
        }

        protected override bool CompareValues(TimeInterval other)
        {
            return (TimeStart, TimeEnd) == (other.TimeStart, other.TimeEnd);
        }
    }
}
