using zms.Common.SharedKernel.Base.Specification;
using zms.Common.SharedKernel.Common.Dating;
using zms.Support.Articles.Domain.OfArticle;

namespace zms.Support.Articles.Domain.OfAttachment
{
    /// <summary>
    /// Фабрика спецификаций для Attachment
    /// </summary>
    public class AttachmentSpecificationFactory
    {
        /// <summary>
        /// Условие по идентификатору
        /// </summary>
        /// <param name="attachmentId">Идентификатор</param>
        /// <returns></returns>
        public static Specification<Attachment> ById(AttachmentId attachmentId) => new AdHocSpecification<Attachment>(a => a.Id == attachmentId);

        /// <summary>
        /// Условие по идентификатору статьи
        /// </summary>
        /// <param name="articleId">Идентификатор</param>
        /// <returns></returns>
        public static Specification<Attachment> ByArticleId(ArticleId articleId) => articleId == ArticleId.Default ? new AdHocSpecification<Attachment>(a => a.ArticleId == null) : new AdHocSpecification<Attachment>(a => a.ArticleId == articleId);

        /// <summary>
        /// Созданные до
        /// </summary>
        /// <param name="date">Дата</param>
        /// <returns></returns>
        public static Specification<Attachment> CreatedBefore(DateWithTime date) => new AdHocSpecification<Attachment>(a => a.CreatedDate < date);
    }
}
