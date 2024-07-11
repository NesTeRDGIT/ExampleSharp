using zms.Common.SharedKernel.Base.Domain;

namespace zms.Support.Articles.Domain.OfAttachment
{
    /// <summary>
    /// Идентификатор в хранилище
    /// </summary>
    public class StorageId : EntityId<StorageId>
    {
        public StorageId(long value)
        {
            Value = value;
        }
    }
}
