using zms.Common.Application.LightRead;
using zms.Support.Articles.Application.Use.ClientQuery.GetArticleListItem;

namespace zms.Support.Articles.Application.Persistence.QueryObject
{
    /// <summary>
    /// Объект запроса списка статей
    /// </summary>
    public interface IGetArticleListItemQueryObject
    {
        /// <summary>
        /// Получить
        /// </summary>
        /// <param name="lightReadParams">Параметры чтения</param>
        /// <returns></returns>
        public Task<IList<ArticleListItem>> GetAsync(LightReadParams<ArticleListItem> lightReadParams);

        /// <summary>
        /// Получить количество
        /// </summary>
        /// <param name="lightReadParams">Параметры чтения</param>
        /// <returns></returns>
        public Task<long> GetCountAsync(LightReadParams<ArticleListItem> lightReadParams);
    }
}
