using zms.Common.SharedKernel.Base.Domain;
using zms.Common.SharedKernel.Common.Dating;
using zms.Common.SharedKernel.Exception;

namespace zms.Support.Articles.Domain.OfArticle
{
    /// <summary>
    /// Статья
    /// </summary>
    public class Article : AggregateRoot<ArticleId>
    {
        private Title title;
        private Content content;

        private Article()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="createdDate">Дата создания</param>
        /// <param name="title">Заголовок</param>
        /// <param name="content">Содержимое</param>
        public Article(ArticleId id, DateWithTime createdDate, Title title, Content content) : this()
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            if (id == ArticleId.Default)
            {
                throw new BadValueDomainException("Идентификатор статьи не может быть пустым");
            }
            CreatedDate = createdDate ?? throw new ArgumentNullException(nameof(createdDate));
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateWithTime CreatedDate { get; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public Title Title
        {
            get => title;
            set => title = value ?? throw new ArgumentNullException(nameof(Title));
        }

        /// <summary>
        /// Содержимое
        /// </summary>
        public Content Content
        {
            get => content;
            set => content = value ?? throw new ArgumentNullException(nameof(Content));
        }
    }
}
