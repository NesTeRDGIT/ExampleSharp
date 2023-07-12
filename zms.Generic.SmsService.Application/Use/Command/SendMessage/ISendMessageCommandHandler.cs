using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.Command.SendMessage
{
    /// <summary>
    /// Обработчик команды рассылки СМС
    /// </summary>
    public interface ISendMessageCommandHandler : ICommandHandler<SendMessageCommand, SendMessageCommandResponse>
    {
      
    }
}
