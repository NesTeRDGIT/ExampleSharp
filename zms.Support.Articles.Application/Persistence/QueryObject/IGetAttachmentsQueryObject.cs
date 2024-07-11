using zms.Support.Articles.Application.Use.ClientQuery.GetAttachments;
using zms.Support.Articles.Domain.OfArticle;

namespace zms.Support.Articles.Application.Persistence.QueryObject
{
    /// <summary>
    /// Объект запроса списка вложений
    /// </summary>
    public interface IGetAttachmentsQueryObject
    {
        /// <summary>
        /// Получить
        /// </summary>
        /// <param name="articleId">Идентификатор</param>
        /// <returns></returns>
        Task<IList<AttachmentProjection>> GetAsync(ArticleId articleId);
    }
}
