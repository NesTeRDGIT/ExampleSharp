using zms.Generic.SmsService.Domain.OfMessage;

namespace zms.Generic.SmsService.Application.Outside.SmsSender
{
    /// <summary>
    /// Интерфейс отправки СМС сообщений
    /// </summary>
    public interface ISmsSender
    {
        /// <summary>
        /// Подключить
        /// </summary>
        /// <returns></returns>
        public Task ConnectAsync();

        /// <summary>
        /// Отправить
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <returns></returns>
        public Task<SendMessageResult> SendAsync(Message message);

        /// <summary>
        /// Отключить
        /// </summary>
        /// <returns></returns>
        public Task DisconnectAsync();
    }
}