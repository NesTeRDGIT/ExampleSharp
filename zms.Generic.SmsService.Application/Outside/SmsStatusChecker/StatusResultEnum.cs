namespace zms.Generic.SmsService.Application.Outside.SmsStatusChecker
{
    /// <summary>
    /// Результат проверки статуса
    /// </summary>
    public enum StatusResultEnum
    {
        /// <summary>
        /// Нет изменений
        /// </summary>
        None = 0,

        /// <summary>
        /// Успешно
        /// </summary>
        Success = 1,

        /// <summary>
        /// Ошибка
        /// </summary>
        Error = 2
    }
}
