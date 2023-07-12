namespace zms.Generic.SmsService.Application.Outside.SmsSender
{
    /// <summary>
    /// Результат отправки сообщения
    /// </summary>
    public class SendMessageResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result">Результат</param>
        /// <param name="comment">Комментарий</param>
        /// <param name="externalId">Внешний идентификатор</param>
        /// <param name="providerName">Наименование провайдера отправки</param>
        public SendMessageResult(SendResultEnum result, string comment, string externalId, string providerName)
        {
            Result = result;
            ProviderName = providerName;
            ExternalId = externalId ?? Domain.OfMessage.ExternalId.Default;
            Comment = comment ?? string.Empty;
        }

        /// <summary>
        /// Результат обработки
        /// </summary>
        public SendResultEnum Result { get; }

        /// <summary>
        /// Наименование провайдера отправки
        /// </summary>
        public string ProviderName { get; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; }

        /// <summary>
        /// Внешний идентификатор
        /// </summary>
        public string ExternalId { get; }
     }
}
