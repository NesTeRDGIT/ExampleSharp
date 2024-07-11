using System.Text;
using Microsoft.EntityFrameworkCore;
using zms.Common.Application.LightRead;
using zms.Common.Application.LightRead.Extensions;
using zms.Persistence.SqlServer.EF.Articles.Context;
using zms.Support.Articles.Application.Persistence.QueryObject;
using zms.Support.Articles.Application.Use.ClientQuery.GetArticleListItem;
using zms.Support.Articles.Domain.OfAttachment;

namespace zms.Persistence.SqlServer.EF.Articles.QueryObject
{
    public class GetArticleListItemQueryObject(ArticlesContext articlesContext) : IGetArticleListItemQueryObject
    {
        private readonly ArticlesContext articlesContext = articlesContext ?? throw new ArgumentNullException(nameof(articlesContext));

        public async Task<IList<ArticleListItem>> GetAsync(LightReadParams<ArticleListItem> lightReadParams)
        {
            return await GetQuery().Apply(lightReadParams).AsNoTracking().ToListAsync();
        }

        public async Task<long> GetCountAsync(LightReadParams<ArticleListItem> lightReadParams)
        {
            return await GetQuery().ApplyFilter(lightReadParams).AsNoTracking().CountAsync();
        }

        private IQueryable<ArticleListItem> GetQuery()
        {
            var query = 
                from article in articlesContext.Articles
                join attachment in articlesContext.Attachments.Where(x=>x.Type.Value  ==  AttachmentType.Default.Value) on article.Id equals attachment.ArticleId into attachmentGroup
                from attachment in attachmentGroup.DefaultIfEmpty()
                group attachment by new { article.Id, article.CreatedDate, Title = article.Title.Value, Content = article.Content.Value } into g
                select new ArticleListItem
                {
                    Id = g.Key.Id,
                    CreatedDate = g.Key.CreatedDate,
                    ContentPart = GetPartContent(g.Key.Content, 600),
                    Title = g.Key.Title,
                    AttachmentCount = g.Select(x => x.Id).Distinct().Count()
                };

            return query;
        }

        /// <summary>
        /// Получить часть контента
        /// </summary>
        /// <param name="content"></param>
        /// <param name="length">Размер в символах</param>
        /// <returns></returns>
        private static string GetPartContent(string content, long length)
        {
            var stringBuilder = new StringBuilder();
            var count = 0;
            var position = 0;
            var htmlTag = 0;

            while (count < length && position < content.Length)
            {
                var c = content[position];
                stringBuilder.Append(c);
                position++;
                switch (c)
                {
                    //Если тэг начался
                    case '<':
                        htmlTag++;
                        break;
                    //Если тэг закончился
                    case '>':
                        htmlTag--;
                        break;
                    default:
                        //Если символ вне тэга, то считаем его в количество
                        if(htmlTag == 0) 
                            count++;
                        break;
                }
            }

            return stringBuilder.ToString();
        }
    }
}
