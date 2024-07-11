using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.Command.SetMessageError
{
    /// <summary>
    /// Обработчик команды отметки сообщения как ошибочного
    /// </summary>
    public interface ISetMessageErrorCommandHandler : ICommandHandler<SetMessageErrorCommand, SetMessageErrorResponse>
    {
      
    }
}
