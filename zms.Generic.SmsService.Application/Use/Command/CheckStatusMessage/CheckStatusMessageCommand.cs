using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.Command.CheckStatusMessage
{
    /// <summary>
    /// Команда проверки статусов отправленных сообщений
    /// </summary>
    public class CheckStatusMessageCommand : ICommand<CheckStatusMessageCommandResponse>
    {
    }
}
