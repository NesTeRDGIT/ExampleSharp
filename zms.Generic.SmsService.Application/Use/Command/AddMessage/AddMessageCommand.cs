using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.Command.AddMessage
{
    /// <summary>
    /// Команда добавления сообщения
    /// </summary>
    public class AddMessageCommand : ICommand<AddMessageCommandResponse>
    {
        /// <summary>
        /// Сообщения
        /// </summary>
        public List<MessageProjection> Messages { get; set; }
    }
}
