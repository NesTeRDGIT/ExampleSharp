using zms.Common.Application.Base.Cqrs;

namespace zms.Support.Articles.Application.Use.Command.AddAttachment
{
    /// <summary>
    /// Команда добавления вложения
    /// </summary>
    public class AddAttachmentCommand : ICommand<AddAttachmentResponse>
    {
        /// <summary>
        /// Идентификатор статьи
        /// </summary>
        public long? ArticleId { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Данные
        /// </summary>
        public byte[] Data { get; set; }
    }
}
