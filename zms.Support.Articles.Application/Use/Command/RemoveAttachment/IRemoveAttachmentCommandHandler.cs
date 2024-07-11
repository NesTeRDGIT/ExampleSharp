using zms.Common.Application.Base.Cqrs;

namespace zms.Support.Articles.Application.Use.Command.RemoveAttachment
{
    /// <summary>
    /// Обработчик команды удаления вложения
    /// </summary>
    public interface IRemoveAttachmentCommandHandler : ICommandHandler<RemoveAttachmentCommand>;
}
