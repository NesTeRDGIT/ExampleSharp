using zms.Common.Application.Base.Cqrs.OfCollectionQuery;
using zms.Common.Application.LightRead;

namespace zms.Support.Articles.Application.Use.ClientQuery.GetArticleListItem
{
    /// <summary>
    /// Запрос списка статей
    /// </summary>
    public class GetArticleListItemQuery : CollectionQuery<ArticleListItem>
    {
        /// <summary>
        /// Параметры чтения
        /// </summary>
        public LightReadParams<ArticleListItem> LightReadParams { get; set; }
    }
}
