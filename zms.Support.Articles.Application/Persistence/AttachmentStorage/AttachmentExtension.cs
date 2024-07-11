using zms.Support.Articles.Domain.OfAttachment;

namespace zms.Support.Articles.Application.Persistence.AttachmentStorage
{
    /// <summary>
    /// Расширение для Attachment
    /// </summary>
    public static class AttachmentExtension
    {
        /// <summary>
        /// Получить метаданные
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public static AttachmentMetadata GetMetadata(this Attachment attachment)
        {
            return new AttachmentMetadata(attachment.ArticleId, attachment.Name.Value);
        }
    }
}
