using zms.Support.Articles.Domain.OfAttachment;

namespace zms.Support.Articles.Application.Persistence.AttachmentStorage
{
    /// <summary>
    /// Хранилище данных вложений
    /// </summary>
    public interface IAttachmentStorage
    {
        /// <summary>
        /// Отправить данные в хранилище
        /// </summary>
        /// <param name="data">Данные вложения</param>
        /// <param name="metadata">Метаданные вложения</param>
        /// <returns>Идентификатор в хранилище</returns>
        public Task<StorageId> SendAsync(byte[] data, AttachmentMetadata metadata);

        /// <summary>
        /// Удалить данные из хранилища
        /// </summary>
        /// <param name="storageId">Идентификатор в хранилище</param>
        /// <param name="metadata">Метаданные вложения</param>
        /// <returns></returns>
        public Task RemoveAsync(StorageId storageId, AttachmentMetadata metadata);

        /// <summary>
        /// Обновить метаданные
        /// </summary>
        /// <param name="storageId">Идентификатор в хранилище</param>
        /// <param name="metadata">Метаданные вложения</param>
        /// <returns></returns>
        public Task UpdateAsync(StorageId storageId, AttachmentMetadata metadata);

        /// <summary>
        /// Получить данные из хранилища
        /// </summary>
        /// <param name="storageId">Идентификатор в хранилище</param>
        /// <returns></returns>
        public Task<Stream> GetReadStreamAsync(StorageId storageId);
    }
}
