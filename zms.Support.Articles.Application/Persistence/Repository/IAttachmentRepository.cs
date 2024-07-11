using zms.Common.SharedKernel.Base.Specification;
using zms.Support.Articles.Domain.OfAttachment;

namespace zms.Support.Articles.Application.Persistence.Repository
{
    /// <summary>
    /// Хранилище вложений
    /// </summary>
    public interface IAttachmentRepository
    {
        /// <summary>
        /// Добавить
        /// </summary>
        /// <param name="attachment">Вложение</param>
        /// <param name="unitOfWork">Единица работы с хранилищем</param>
        Task AddAsync(Attachment attachment, IUnitOfWork unitOfWork);

        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="attachment">Вложение</param>
        /// <param name="unitOfWork">Единица работы с хранилищем</param>
        /// <returns></returns>
        Task RemoveAsync(Attachment attachment, IUnitOfWork unitOfWork);

        /// <summary>
        /// Обновить
        /// </summary>
        /// <param name="attachment">Вложение</param>
        /// <param name="unitOfWork">Единица работы с хранилищем</param>
        Task UpdateAsync(Attachment attachment, IUnitOfWork unitOfWork);

        /// <summary>
        /// Получить
        /// </summary>
        /// <param name="specification">Спецификация</param>
        /// <param name="unitOfWork">Единица работы с хранилищем</param>
        /// <returns></returns>
        Task<IList<Attachment>> GetAsync(Specification<Attachment> specification, IUnitOfWork unitOfWork);

        /// <summary>
        /// Существует ли элемент
        /// </summary>
        /// <param name="specification">Спецификация</param>
        /// <param name="unitOfWork">Единица работы с хранилищем</param>
        /// <returns></returns>
        Task<bool> ExistAsync(Specification<Attachment> specification, IUnitOfWork unitOfWork);
    }
}
