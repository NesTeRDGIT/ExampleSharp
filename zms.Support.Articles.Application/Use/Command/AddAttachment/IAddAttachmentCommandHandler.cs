using zms.Common.Application.Base.Cqrs;

namespace zms.Support.Articles.Application.Use.Command.AddAttachment
{
    /// <summary>
    /// Обработчик команды добавления вложения
    /// </summary>
    public interface IAddAttachmentCommandHandler : ICommandHandler<AddAttachmentCommand, AddAttachmentResponse>;
}
