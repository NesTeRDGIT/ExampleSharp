using zms.Common.SharedKernel.Base.Specification;

namespace zms.Support.Articles.Domain.OfArticle
{
    /// <summary>
    /// Фабрика спецификаций для Article
    /// </summary>
    public class ArticleSpecificationFactory
    {
        /// <summary>
        /// Условие по идентификатору
        /// </summary>
        /// <param name="articleId">Идентификатор</param>
        /// <returns></returns>
        public static Specification<Article> ById(ArticleId articleId) => new AdHocSpecification<Article>(a => a.Id == articleId);
    }
}
