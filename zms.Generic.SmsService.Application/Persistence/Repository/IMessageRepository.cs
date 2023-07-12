using zms.Common.SharedKernel.Base.Specification;
using zms.Generic.SmsService.Domain.OfMessage;

namespace zms.Generic.SmsService.Application.Persistence.Repository
{
    /// <summary>
    /// Репозиторий сообщений
    /// </summary>
    public interface IMessageRepository
    {
        /// <summary>
        /// Добавить
        /// </summary>
        /// <param name="message">Сообщения</param>
        /// <param name="unitOfWork">Единица работы хранилищем</param>
        /// <returns></returns>
        Task AddAsync(Message message, IUnitOfWork unitOfWork);

        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="message">Сообщения</param>
        /// <param name="unitOfWork">Единица работы хранилищем</param>
        /// <returns></returns>
        Task RemoveAsync(Message message, IUnitOfWork unitOfWork);

        /// <summary>
        /// Обновить
        /// </summary>
        /// <param name="message">Сообщения</param>
        /// <param name="unitOfWork">Единица работы хранилищем</param>
        /// <returns></returns>
        Task UpdateAsync(Message message, IUnitOfWork unitOfWork);

        /// <summary>
        /// Получить
        /// </summary>
        /// <param name="specification">Спецификация</param>
        /// <param name="unitOfWork">Единица работы хранилищем</param>
        /// <returns></returns>
        Task<IList<Message>> GetAsync(Specification<Message> specification, IUnitOfWork unitOfWork);

        /// <summary>
        /// Существует ли
        /// </summary>
        /// <param name="specification">Спецификация</param>
        /// <param name="unitOfWork">Единица работы хранилищем</param>
        /// <returns></returns>
        Task<bool> ExistAsync(Specification<Message> specification, IUnitOfWork unitOfWork);
    }
}
