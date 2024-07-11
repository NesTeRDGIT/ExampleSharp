using zms.Support.Articles.Domain.OfArticle;

namespace zms.Support.Articles.Application.Persistence.AttachmentStorage
{
    /// <summary>
    /// Метаданные вложения
    /// </summary>
    public class AttachmentMetadata
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleId">Идентификатор статьи</param>
        /// <param name="name">Наименование вложения</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public AttachmentMetadata(ArticleId articleId, string name)
        {
            ArticleId = articleId ?? throw new ArgumentNullException(nameof(articleId));
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException("Наименование вложения не может быть пустым");
        }

        /// <summary>
        /// Идентификатор статьи
        /// </summary>
        public ArticleId ArticleId { get; }

        /// <summary>
        /// Наименование вложения
        /// </summary>
        public string Name { get; }
    }
}
