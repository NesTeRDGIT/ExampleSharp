using Microsoft.EntityFrameworkCore;
using zms.Common.SharedKernel.Base.Domain;
using zms.Persistence.SqlServer.EF.Articles.Context;
using zms.Persistence.SqlServer.EF.Common.IdGeneration;
using zms.Support.Articles.Domain;
using zms.Support.Articles.Domain.OfArticle;
using zms.Support.Articles.Domain.OfAttachment;

namespace zms.Persistence.SqlServer.EF.Articles
{
    /// <summary>
    /// Генератор Id Hi-Lo
    /// </summary>
    public class IdGenerator : EntityIdGeneratorBase, IIdGenerator
    {
        private readonly DbContextOptions<ArticlesContext> options;

        public IdGenerator(DbContextOptions<ArticlesContext> options)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            AddKeyParameter(typeof(ArticleId), "ArticleHiLo");
            AddKeyParameter(typeof(AttachmentId), "AttachmentHiLo");
        }

        public async Task<TEntityId> NewIdAsync<TEntityId>() where TEntityId : IEntityId
        {
            await using var context = new ArticlesContext(options);
            return await NewId<TEntityId>(context);
        }
    }
}
