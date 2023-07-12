using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.Command.SendMessage
{
    /// <summary>
    /// Команда рассылки СМС
    /// </summary>
    public class SendMessageCommand : ICommand<SendMessageCommandResponse>
    {
    }
}
