using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.Command.AddMessage
{
    /// <summary>
    /// Обработчик команды добавления сообщения
    /// </summary>
    public interface IAddMessageCommandHandler : ICommandHandler<AddMessageCommand, AddMessageCommandResponse>
    {
      
    }
}
