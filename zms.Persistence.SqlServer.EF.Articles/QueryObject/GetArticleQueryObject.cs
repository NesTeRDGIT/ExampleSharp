using Microsoft.EntityFrameworkCore;
using zms.Persistence.SqlServer.EF.Articles.Context;
using zms.Support.Articles.Application.Persistence.QueryObject;
using zms.Support.Articles.Application.Use.ClientQuery.GetArticle;
using zms.Support.Articles.Domain.OfArticle;

namespace zms.Persistence.SqlServer.EF.Articles.QueryObject
{
    public class GetArticleQueryObject(ArticlesContext articlesContext) : IGetArticleQueryObject
    {
        private readonly ArticlesContext articlesContext = articlesContext ?? throw new ArgumentNullException(nameof(articlesContext));

        public async Task<ArticleProjection> GetAsync(ArticleId articleId)
        {
            var query =
                from article in articlesContext.Articles
                where article.Id == articleId
                select new ArticleProjection
                {
                    Id = article.Id,
                    Title = article.Title.Value,
                    CreatedDate = article.CreatedDate,
                    Content = article.Content.Value
                };

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
