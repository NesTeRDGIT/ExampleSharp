using zms.Common.SharedKernel.Base.Specification;
using zms.Support.Articles.Domain.OfArticle;

namespace zms.Support.Articles.Application.Persistence.Repository
{
    /// <summary>
    /// Хранилище статей
    /// </summary>
    public interface IArticleRepository
    {
        /// <summary>
        /// Добавить
        /// </summary>
        /// <param name="article">Статья</param>
        /// <param name="unitOfWork">Единица работы с хранилищем</param>
        Task AddAsync(Article article, IUnitOfWork unitOfWork);

        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="article">Статья</param>
        /// <param name="unitOfWork">Единица работы с хранилищем</param>
        /// <returns></returns>
        Task RemoveAsync(Article article, IUnitOfWork unitOfWork);

        /// <summary>
        /// Обновить
        /// </summary>
        /// <param name="article">Статья</param>
        /// <param name="unitOfWork">Единица работы с хранилищем</param>
        Task UpdateAsync(Article article, IUnitOfWork unitOfWork);

        /// <summary>
        /// Получить
        /// </summary>
        /// <param name="specification">Спецификация</param>
        /// <param name="unitOfWork">Единица работы с хранилищем</param>
        /// <returns></returns>
        Task<IList<Article>> GetAsync(Specification<Article> specification, IUnitOfWork unitOfWork);

        /// <summary>
        /// Существует ли элемент
        /// </summary>
        /// <param name="specification">Спецификация</param>
        /// <param name="unitOfWork">Единица работы с хранилищем</param>
        /// <returns></returns>
        Task<bool> ExistAsync(Specification<Article> specification, IUnitOfWork unitOfWork);
    }
}
