using zms.Common.SharedKernel.Base.Domain;

namespace zms.Support.Articles.Domain.OfAttachment
{
    /// <summary>
    /// Идентификатор вложения
    /// </summary>
    public class AttachmentId : EntityId<AttachmentId>
    {
        public AttachmentId(long value)
        {
            Value = value;
        }
    }
}
