using Microsoft.EntityFrameworkCore;
using zms.Common.SharedKernel.Base.Specification;
using zms.Persistence.SqlServer.EF.Articles.Context;
using zms.Support.Articles.Application.Persistence.Repository;
using zms.Support.Articles.Domain.OfArticle;

namespace zms.Persistence.SqlServer.EF.Articles.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        public async Task AddAsync(Article article, IUnitOfWork unitOfWork)
        {
            var articlesContext = GetDbContext(unitOfWork);
            await articlesContext.AddAsync(article);
        }

        public Task RemoveAsync(Article article, IUnitOfWork unitOfWork)
        {
            var articlesContext = GetDbContext(unitOfWork);
            return Task.FromResult(articlesContext.Remove(article));
        }

        public Task UpdateAsync(Article article, IUnitOfWork unitOfWork)
        {
            return Task.CompletedTask;
        }

        public async Task<IList<Article>> GetAsync(Specification<Article> specification, IUnitOfWork unitOfWork)
        {
            var articlesContext = GetDbContext(unitOfWork);
            return await articlesContext.Articles.Where(specification.ToExpression()).ToListAsync();
        }

        public async Task<bool> ExistAsync(Specification<Article> specification, IUnitOfWork unitOfWork)
        {
            var expertContext = GetDbContext(unitOfWork);
            return await expertContext.Articles.Where(specification.ToExpression()).CountAsync() != 0;
        }

        private ArticlesContext GetDbContext(IUnitOfWork unitOfWork)
        {
            if (unitOfWork is not ArticlesContext ctx)
                throw new InvalidCastException("Объект unitOfWork не соответствует реализации репозитория. Ожидается объект типа ArticlesContext");
            return ctx;
        }
    }
}
