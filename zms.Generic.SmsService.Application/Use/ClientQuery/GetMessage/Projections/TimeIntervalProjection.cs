

namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetMessage.Projections
{
    /// <summary>
    /// Проекция интервала времени
    /// </summary>
    public class TimeIntervalProjection
    {
        /// <summary>
        /// Время начала
        /// </summary>
        public string TimeStart { get; set; }

        /// <summary>
        /// Время окончания
        /// </summary>
        public string TimeEnd { get; set; }
    }
}
