using zms.Common.Application.Base.Cqrs;

namespace zms.Support.Articles.Application.Use.Command.AddAttachment
{
    /// <summary>
    /// Результат команды добавления вложения
    /// </summary>
    public class AddAttachmentResponse : IResponse
    {
        /// <summary>
        /// Идентификатор вложения
        /// </summary>
        public long AttachmentId { get; set; }
    }
}
