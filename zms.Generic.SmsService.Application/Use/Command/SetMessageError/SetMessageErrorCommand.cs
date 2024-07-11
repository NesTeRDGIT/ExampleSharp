using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.Command.SetMessageError
{
    /// <summary>
    /// Команда отметки сообщения как ошибочного
    /// </summary>
    public class SetMessageErrorCommand : ICommand<SetMessageErrorResponse>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }
    }
}
