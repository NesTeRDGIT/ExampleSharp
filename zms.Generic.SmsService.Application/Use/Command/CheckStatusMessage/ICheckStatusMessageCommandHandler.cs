using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.Command.CheckStatusMessage
{
    /// <summary>
    /// Обработчик команды проверки статусов отправленных сообщений
    /// </summary>
    public interface ICheckStatusMessageCommandHandler : ICommandHandler<CheckStatusMessageCommand, CheckStatusMessageCommandResponse>
    {
      
    }
}
