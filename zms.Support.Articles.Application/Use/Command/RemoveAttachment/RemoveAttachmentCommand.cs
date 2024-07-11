using zms.Common.Application.Base.Cqrs;

namespace zms.Support.Articles.Application.Use.Command.RemoveAttachment
{
    /// <summary>
    /// Команда удаления вложения
    /// </summary>
    public class RemoveAttachmentCommand : ICommand
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }
    }
}
