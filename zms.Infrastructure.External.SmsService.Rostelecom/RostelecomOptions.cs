namespace zms.Infrastructure.External.SmsService.Rostelecom
{
    /// <summary>
    /// Параметры данных Rostelecom
    /// </summary>
    public class RostelecomOptions
    {
        /// <summary>
        /// Хост
        /// </summary>
        public string Host { get; set; } = string.Empty;

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public  string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public  string UserPassword { get; set; } = string.Empty;

        /// <summary>
        /// Имя отправителя из аккаунта Beeline
        /// </summary>
        public string Sender { get; set; } = string.Empty;

        /// <summary>
        /// Часовой пояс
        /// </summary>
        public int TimeZone { get; set; }
    }
}
