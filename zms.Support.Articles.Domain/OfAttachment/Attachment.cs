using zms.Common.SharedKernel.Base.Domain;
using zms.Common.SharedKernel.Common.Dating;
using zms.Common.SharedKernel.Exception;
using zms.Support.Articles.Domain.OfArticle;

namespace zms.Support.Articles.Domain.OfAttachment
{
    /// <summary>
    /// Вложение
    /// </summary>
    public class Attachment : AggregateRoot<AttachmentId>
    {
        private Attachment()
        {
            
        }

        /// <summary>
        /// Создать вложение
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="articleId">Идентификатор статьи</param>
        /// <param name="type">Тип вложения</param>
        /// <param name="createdDate">Дата загрузки вложения</param>
        /// <param name="name">Наименование файла</param>
        /// <param name="storageId">Идентификатор в хранилище</param>
        public Attachment(AttachmentId id, ArticleId articleId, StorageId storageId, AttachmentType type, DateWithTime createdDate,  Name name) : this()
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            ArticleId = articleId ?? throw new ArgumentNullException(nameof(articleId));
            CreatedDate = createdDate ?? throw new ArgumentNullException(nameof(createdDate));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            StorageId = storageId ?? throw new ArgumentNullException(nameof(storageId));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            if (Type == AttachmentType.Default && ArticleId == ArticleId.Default)
            {
                throw new BadValueDomainException("Для не системного вложения идентификатор статьи не может быть пустым");
            }
        }

        /// <summary>
        /// Идентификатор статьи
        /// </summary>
        public ArticleId ArticleId { get; private set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateWithTime CreatedDate { get; }

        /// <summary>
        /// Тип
        /// </summary>
        public AttachmentType Type { get; }

        /// <summary>
        /// Наименование файла
        /// </summary>
        public Name Name { get; }

        /// <summary>
        /// Идентификатор в хранилище
        /// </summary>
        public StorageId StorageId { get; }

        /// <summary>
        /// Изменить идентификатор статьи
        /// </summary>
        /// <remarks>Только для системных вложений</remarks>
        public void ChangeArticleId(ArticleId articleId)
        {
            ArgumentNullException.ThrowIfNull(articleId, nameof(articleId));
            if (Type != AttachmentType.System)
            {
                throw new InvalidOperationDomainException("Не допустимо менять статью не системному вложению");
            }
            ArticleId = articleId;
        }
    }
}
