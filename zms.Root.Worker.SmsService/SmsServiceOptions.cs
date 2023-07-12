namespace zms.Root.Worker.SmsService
{
    /// <summary>
    /// Параметры сервиса
    /// </summary>
    public class SmsServiceOptions
    {
        /// <summary>
        /// Перерыв между отправками
        /// </summary>
        public int TimeOut { get; set; } = 10000;
    }
}
