using zms.Common.SharedKernel.Base.Domain;

namespace zms.Support.Articles.Domain.OfArticle
{
    /// <summary>
    /// Идентификатор статьи
    /// </summary>
    public class ArticleId : EntityId<ArticleId>
    {
        private ArticleId()
        {
            value = null;
        }

        public ArticleId(long value)
        {
            Value = value;
        }

        /// <summary>
        /// Значение по умолчанию
        /// </summary>
        /// <returns></returns>
        public static ArticleId Default => new ();
    }
}
