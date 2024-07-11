using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.Command.SetMessageError
{
    /// <summary>
    /// Результат команды отметки сообщения как ошибочного
    /// </summary>
    public class SetMessageErrorResponse : IResponse
    {
        /// <summary>
        /// Идентификаторы
        /// </summary>
        public List<long> Id { get; set; }
    }
}
