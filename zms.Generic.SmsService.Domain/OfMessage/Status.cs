using zms.Common.SharedKernel.Base.Domain;

namespace zms.Generic.SmsService.Domain.OfMessage
{
    /// <summary>
    /// Статус сообщения
    /// </summary>
    public class Status : StaticReference<Status, string>
    {
        private Status(string value, string name)
        {
            Value = value;
            Name = name;
        }


        /// <summary>
        /// Новое
        /// </summary>
        public static Status New => new("new", "Новое");

        /// <summary>
        /// В обработке
        /// </summary>
        public static Status InProcessing => new("inProcessing", "В обработке");
        
        /// <summary>
        /// Отправлено
        /// </summary>
        public static Status Sent => new("sent", "Отправлено");

        /// <summary>
        /// Ошибка отправки
        /// </summary>
        public static Status Error => new("error", "Ошибка отправки");
    }
}
