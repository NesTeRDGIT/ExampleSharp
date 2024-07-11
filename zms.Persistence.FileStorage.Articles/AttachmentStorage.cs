using zms.Common.SharedKernel.Exception;
using zms.Infrastructure.Utility.FileStorage.Locations;
using zms.Persistence.FileStorage.Articles.Config;
using zms.Support.Articles.Application.Persistence.AttachmentStorage;
using zms.Support.Articles.Domain.OfArticle;
using zms.Support.Articles.Domain.OfAttachment;

namespace zms.Persistence.FileStorage.Articles
{
    public class AttachmentStorage(StorageLocation storageLocation) : IAttachmentStorage
    {
        private readonly StorageLocation storageLocation = storageLocation ?? throw new ArgumentNullException(nameof(storageLocation));

        public async Task<StorageId> SendAsync(byte[] data, AttachmentMetadata metadata)
        {
            var location = GetLocation(metadata);
            var file = await location.CreateFileAsync(metadata.Name);
            await file.WriteDataAsync(data);
            return new StorageId(file.Id);
        }

        public async Task RemoveAsync(StorageId storageId, AttachmentMetadata metadata)
        {
            var file = await storageLocation.GetFileAsync(storageId.Value);
            if (file != null)
            {
                await storageLocation.RemoveFileAsync(file);
            }
        }

        public async Task UpdateAsync(StorageId storageId, AttachmentMetadata metadata)
        {
            //Переносим вложение в каталог статьи
            var location = GetLocation(metadata);
            var file = await storageLocation.GetFileAsync(storageId.Value);
            if (file != null && file.Location != location.FullName)
            {
                await file.MoveToLocation(location);
            }
        }

        public async Task<Stream> GetReadStreamAsync(StorageId storageId)
        {
            var file = await storageLocation.GetFileAsync(storageId);
            if (file == null)
            {
                throw new EntityNotExistDomainException($"Не удалось найти файл с идентификатором: {storageId.Value} в хранилище");
            }
            return file.GetReadStream();
        }

        private Location<StorageLocation> GetLocation(AttachmentMetadata metadata)
        {
            var location = storageLocation.Attachments;
            if (metadata.ArticleId != ArticleId.Default)
            {
                location = location.GetChildLocation(metadata.ArticleId.Value.ToString());
            }
            return location;
        }
    }
}
