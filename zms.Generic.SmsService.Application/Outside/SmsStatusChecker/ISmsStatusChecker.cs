using zms.Generic.SmsService.Domain.OfMessage;

namespace zms.Generic.SmsService.Application.Outside.SmsStatusChecker
{
    /// <summary>
    /// Интерфейс проверки статуса СМС сообщений
    /// </summary>
    public interface ISmsStatusChecker
    {
        /// <summary>
        /// Подключить
        /// </summary>
        /// <returns></returns>
        public Task ConnectAsync();

        /// <summary>
        /// Проверить статус
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <returns></returns>
        public Task<StatusMessageResult> GetStatusAsync(Message message);

        /// <summary>
        /// Отключить
        /// </summary>
        /// <returns></returns>
        public Task DisconnectAsync();
    }
}